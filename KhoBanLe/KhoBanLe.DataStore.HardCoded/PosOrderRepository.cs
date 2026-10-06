using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class PosOrderRepository : IPosOrderRepository
    {
        private readonly List<PosOrder> _orders;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IGoodsIssueRepository _goodsIssueRepository;

        public PosOrderRepository(
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IGoodsIssueRepository goodsIssueRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _goodsIssueRepository = goodsIssueRepository;

            _orders = new List<PosOrder>
            {
                new PosOrder
                {
                    Id = 1,
                    OrderNumber = "POS-2026-001",
                    CustomerId = 1,
                    CustomerName = "Khách mua lẻ tại quầy",
                    OrderDate = DateTime.Now.AddDays(-3),
                    CashierName = "Thu ngân Nguyễn Thị Mai",
                    PaymentMethod = "Tiền mặt",
                    SubTotal = 1320000,
                    DiscountAmount = 0,
                    TotalAmount = 1320000,
                    ReceivedAmount = 1350000,
                    ChangeAmount = 30000,
                    GoodsIssueId = 1, // Liên kết với PX-2026-001
                    Status = "Paid",
                    Note = "Hóa đơn bán lẻ quầy thu ngân số 1",
                    Lines = new List<PosOrderLine>
                    {
                        new PosOrderLine
                        {
                            Id = 1,
                            PosOrderId = 1,
                            ProductId = 2, // Mì Hảo Hảo
                            Quantity = 10,
                            UnitPrice = 115000
                        },
                        new PosOrderLine
                        {
                            Id = 2,
                            PosOrderId = 1,
                            ProductId = 3, // Sữa Vinamilk
                            Quantity = 5,
                            UnitPrice = 34000
                        }
                    }
                },
                new PosOrder
                {
                    Id = 2,
                    OrderNumber = "POS-2026-002",
                    CustomerId = 2,
                    CustomerName = "Anh Hoàng Long",
                    OrderDate = DateTime.Now.AddDays(-1),
                    CashierName = "Thu ngân Lê Văn Phát",
                    PaymentMethod = "Chuyển khoản QR",
                    SubTotal = 243000,
                    DiscountAmount = 0,
                    TotalAmount = 243000,
                    ReceivedAmount = 243000,
                    ChangeAmount = 0,
                    GoodsIssueId = null,
                    Status = "Paid",
                    Note = "Thanh toán qua quét mã VietQR",
                    Lines = new List<PosOrderLine>
                    {
                        new PosOrderLine
                        {
                            Id = 3,
                            PosOrderId = 2,
                            ProductId = 5, // Gạo ST25 5kg
                            Quantity = 1,
                            UnitPrice = 185000
                        },
                        new PosOrderLine
                        {
                            Id = 4,
                            PosOrderId = 2,
                            ProductId = 6, // Dầu ăn Simply
                            Quantity = 1,
                            UnitPrice = 58000
                        }
                    }
                }
            };
        }

        public async Task<IEnumerable<PosOrder>> GetPosOrdersAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<PosOrder> query = _orders;

            if (fromDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= toDate.Value);
            }

            var list = query.OrderByDescending(o => o.OrderDate).ToList();

            // Populate Product & Customer references
            foreach (var order in list)
            {
                if (order.CustomerId.HasValue && order.Customer == null)
                {
                    order.Customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerId.Value);
                }
                foreach (var line in order.Lines)
                {
                    if (line.Product == null)
                    {
                        line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                    }
                }
            }

            return list;
        }

        public async Task<PosOrder?> GetPosOrderByIdAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                if (order.CustomerId.HasValue && order.Customer == null)
                {
                    order.Customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerId.Value);
                }
                foreach (var line in order.Lines)
                {
                    if (line.Product == null)
                    {
                        line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                    }
                }
            }
            return order;
        }

        public async Task<PosOrder?> GetPosOrderByNumberAsync(string orderNumber)
        {
            var order = _orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber, StringComparison.OrdinalIgnoreCase));
            if (order != null)
            {
                if (order.CustomerId.HasValue && order.Customer == null)
                {
                    order.Customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerId.Value);
                }
                foreach (var line in order.Lines)
                {
                    if (line.Product == null)
                    {
                        line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                    }
                }
            }
            return order;
        }

        public Task<int> CreatePosOrderAsync(PosOrder order)
        {
            lock (_orders)
            {
                int maxId = _orders.Any() ? _orders.Max(o => o.Id) : 0;
                order.Id = maxId + 1;
                if (string.IsNullOrWhiteSpace(order.OrderNumber))
                {
                    order.OrderNumber = $"POS-{DateTime.Now.Year}-{order.Id:D3}";
                }

                int lineId = 1;
                foreach (var line in order.Lines)
                {
                    line.Id = lineId++;
                    line.PosOrderId = order.Id;
                }

                _orders.Add(order);
                return Task.FromResult(order.Id);
            }
        }
    }
}
