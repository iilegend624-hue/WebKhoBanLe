using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.Services
{
    public class CheckoutResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? PosOrderNumber { get; set; }
        public string? IssueNumber { get; set; }
        public double SubTotal { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double ChangeAmount { get; set; }
        public int TotalItems { get; set; }
        public string PaymentMethod { get; set; } = "Tiền mặt";
        public string CustomerName { get; set; } = "Khách mua lẻ tại quầy";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<CartItem> PurchasedItems { get; set; } = new();
    }

    public class ShoppingCartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IGoodsIssueRepository _goodsIssueRepository;
        private readonly IPosOrderRepository _posOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly object _lock = new();

        public List<CartItem> Items { get; private set; } = new();

        public int TotalItems
        {
            get
            {
                lock (_lock)
                {
                    return Items.Sum(i => i.Quantity);
                }
            }
        }

        public double TotalAmount
        {
            get
            {
                lock (_lock)
                {
                    return Items.Sum(i => i.LineTotal);
                }
            }
        }

        public bool IsDrawerOpen { get; private set; } = false;
        public string? LastMessage { get; private set; }

        public event Action? OnChange;

        public ShoppingCartService(
            IProductRepository productRepository,
            IGoodsIssueRepository goodsIssueRepository,
            IPosOrderRepository posOrderRepository,
            ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _goodsIssueRepository = goodsIssueRepository;
            _posOrderRepository = posOrderRepository;
            _customerRepository = customerRepository;
        }

        public void OpenDrawer()
        {
            IsDrawerOpen = true;
            NotifyStateChanged();
        }

        public void CloseDrawer()
        {
            IsDrawerOpen = false;
            NotifyStateChanged();
        }

        public void ToggleDrawer()
        {
            IsDrawerOpen = !IsDrawerOpen;
            NotifyStateChanged();
        }

        public string AddToCart(Product product, int quantity = 1, bool autoOpenDrawer = false)
        {
            if (quantity <= 0) quantity = 1;

            if (product.CurrentStock <= 0)
            {
                return $"Sản phẩm '{product.Name}' hiện đã hết hàng, không thể thêm vào giỏ!";
            }

            lock (_lock)
            {
                var existing = Items.FirstOrDefault(i => i.ProductId == product.Id);
                int newTotalQty = (existing?.Quantity ?? 0) + quantity;

                if (newTotalQty > product.CurrentStock)
                {
                    return $"Tồn kho sản phẩm '{product.Name}' chỉ còn {product.CurrentStock} {product.Unit}, không thể thêm quá số lượng tồn!";
                }

                if (existing != null)
                {
                    existing.Quantity = newTotalQty;
                }
                else
                {
                    Items.Add(new CartItem
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = quantity,
                        UnitPrice = product.Price
                    });
                }

                LastMessage = $"Đã thêm {quantity} {product.Unit} '{product.Name}' vào giỏ!";
                if (autoOpenDrawer)
                {
                    IsDrawerOpen = true;
                }
            }

            NotifyStateChanged();
            return string.Empty; // Success
        }

        public void RemoveFromCart(int productId)
        {
            lock (_lock)
            {
                Items.RemoveAll(i => i.ProductId == productId);
            }
            NotifyStateChanged();
        }

        public string UpdateQuantity(int productId, int quantity)
        {
            lock (_lock)
            {
                var item = Items.FirstOrDefault(i => i.ProductId == productId);
                if (item == null) return "Không tìm thấy mặt hàng trong giỏ.";

                if (quantity <= 0)
                {
                    Items.RemoveAll(i => i.ProductId == productId);
                    NotifyStateChanged();
                    return string.Empty;
                }

                if (item.Product != null && quantity > item.Product.CurrentStock)
                {
                    return $"Tồn kho chỉ còn {item.Product.CurrentStock} {item.Product.Unit}, không thể đặt {quantity}!";
                }

                item.Quantity = quantity;
            }
            NotifyStateChanged();
            return string.Empty;
        }

        public void ClearCart()
        {
            lock (_lock)
            {
                Items.Clear();
            }
            NotifyStateChanged();
        }

        /// <summary>
        /// BÁN HÀNG & THANH TOÁN (Point of Sale / POS):
        /// 1. Kiểm tra tồn kho hàng hóa (Chặn xuất âm).
        /// 2. Tạo chứng từ xuất kho bán lẻ (GoodsIssue).
        /// 3. Phê duyệt & trừ tồn kho trong 1 transaction an toàn (Luật 1).
        /// 4. Tạo Hóa đơn POS (PosOrder & PosOrderLine) liên kết trực tiếp với GoodsIssue.
        /// 5. Xóa giỏ hàng và trả về biên lai bán lẻ đầy đủ.
        /// </summary>
        public async Task<CheckoutResult> CheckoutAsync(
            string customerName = "Khách mua lẻ tại quầy",
            string paymentMethod = "Tiền mặt",
            int? customerId = null,
            double receivedAmount = 0,
            double discountAmount = 0)
        {
            if (!Items.Any())
            {
                return new CheckoutResult { Success = false, ErrorMessage = "Giỏ hàng đang trống!" };
            }

            // Copy items to snapshot
            var purchasedSnapshot = Items.Select(i => new CartItem
            {
                ProductId = i.ProductId,
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            // 1. Kiểm tra tồn kho trước khi thanh toán
            foreach (var item in purchasedSnapshot)
            {
                var prod = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (prod == null)
                {
                    return new CheckoutResult { Success = false, ErrorMessage = $"Sản phẩm mã #{item.ProductId} không tồn tại!" };
                }

                if (prod.CurrentStock < item.Quantity)
                {
                    return new CheckoutResult
                    {
                        Success = false,
                        ErrorMessage = $"Hàng '{prod.Name}' chỉ còn tồn {prod.CurrentStock}, không đủ để bán {item.Quantity}!"
                    };
                }
            }

            double subTotal = purchasedSnapshot.Sum(i => i.LineTotal);
            double totalPayable = Math.Max(0, subTotal - discountAmount);
            double actualReceived = receivedAmount > 0 ? receivedAmount : totalPayable;
            double change = Math.Max(0, actualReceived - totalPayable);

            // 2. Tạo phiếu xuất kho (GoodsIssue)
            var issueLines = purchasedSnapshot.Select(i => new GoodsIssueLine
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            string custName = string.IsNullOrWhiteSpace(customerName) ? "Khách mua lẻ tại quầy" : customerName;

            var issue = new GoodsIssue
            {
                CustomerOrDepartment = custName,
                CreatedBy = "Thu ngân / Bán lẻ POS",
                Note = $"Bán lẻ POS thanh toán qua {paymentMethod}",
                Lines = issueLines
            };

            int issueId = await _goodsIssueRepository.CreateGoodsIssueAsync(issue);

            // 3. Phê duyệt & trừ kho tự động trong 1 transaction (Luật 1)
            await _goodsIssueRepository.ApproveIssueTransactionAsync(issueId, "Hệ thống Bán Lẻ (POS)");

            // 4. Tạo Hóa Đơn Bán Lẻ POS (PosOrder)
            var posOrder = new PosOrder
            {
                CustomerId = customerId,
                CustomerName = custName,
                OrderDate = DateTime.Now,
                CashierName = "Thu ngân / Bán lẻ POS",
                PaymentMethod = paymentMethod,
                SubTotal = subTotal,
                DiscountAmount = discountAmount,
                TotalAmount = totalPayable,
                ReceivedAmount = actualReceived,
                ChangeAmount = change,
                GoodsIssueId = issueId,
                Status = "Paid",
                Note = $"Đơn thanh toán tại quầy POS, liên kết phiếu xuất #{issue.IssueNumber}",
                Lines = purchasedSnapshot.Select(i => new PosOrderLine
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            int posOrderId = await _posOrderRepository.CreatePosOrderAsync(posOrder);
            issue.PosOrderId = posOrderId;

            // 5. Xóa giỏ hàng
            ClearCart();

            return new CheckoutResult
            {
                Success = true,
                PosOrderNumber = posOrder.OrderNumber,
                IssueNumber = issue.IssueNumber,
                SubTotal = subTotal,
                DiscountAmount = discountAmount,
                TotalAmount = totalPayable,
                ReceivedAmount = actualReceived,
                ChangeAmount = change,
                TotalItems = purchasedSnapshot.Sum(i => i.Quantity),
                PaymentMethod = paymentMethod,
                CustomerName = custName,
                OrderDate = posOrder.OrderDate,
                PurchasedItems = purchasedSnapshot
            };
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
