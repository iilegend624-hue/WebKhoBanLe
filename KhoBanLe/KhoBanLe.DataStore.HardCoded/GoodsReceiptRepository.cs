using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class GoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly List<GoodsReceipt> _receipts;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;

        public GoodsReceiptRepository(
            IProductRepository productRepository,
            ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;

            _receipts = new List<GoodsReceipt>
            {
                new GoodsReceipt
                {
                    Id = 1,
                    ReceiptNumber = "PN-2026-001",
                    SupplierId = 3, // Acecook
                    ReceiptDate = DateTime.Now.AddDays(-5),
                    CreatedBy = "Thủ kho Nguyễn Văn A",
                    ApprovedBy = "Quản lý Trần Thị B",
                    ApprovedDate = DateTime.Now.AddDays(-5).AddHours(2),
                    Status = ReceiptStatus.Approved,
                    Note = "Nhập hàng định kỳ tháng 10",
                    Lines = new List<GoodsReceiptLine>
                    {
                        new GoodsReceiptLine
                        {
                            Id = 1,
                            GoodsReceiptId = 1,
                            ProductId = 2, // Mì Hảo Hảo
                            Quantity = 50,
                            UnitPrice = 105000
                        }
                    }
                },
                new GoodsReceipt
                {
                    Id = 2,
                    ReceiptNumber = "PN-2026-002",
                    SupplierId = 2, // Vinamilk
                    ReceiptDate = DateTime.Now.AddDays(-2),
                    CreatedBy = "Thủ kho Nguyễn Văn A",
                    ApprovedBy = "Quản lý Trần Thị B",
                    ApprovedDate = DateTime.Now.AddDays(-2).AddHours(1),
                    Status = ReceiptStatus.Approved,
                    Note = "Nhập bổ sung sữa tươi",
                    Lines = new List<GoodsReceiptLine>
                    {
                        new GoodsReceiptLine
                        {
                            Id = 2,
                            GoodsReceiptId = 2,
                            ProductId = 3, // Sữa Vinamilk
                            Quantity = 20,
                            UnitPrice = 30000
                        }
                    }
                },
                new GoodsReceipt
                {
                    Id = 3,
                    ReceiptNumber = "PN-2026-003",
                    SupplierId = 1, // Thiên Long
                    ReceiptDate = DateTime.Now.AddHours(-3),
                    CreatedBy = "Thủ kho Nguyễn Văn A",
                    ApprovedBy = null,
                    ApprovedDate = null,
                    Status = ReceiptStatus.Draft, // Chờ duyệt
                    Note = "Đơn nhập hàng văn phòng phẩm mới lập",
                    Lines = new List<GoodsReceiptLine>
                    {
                        new GoodsReceiptLine
                        {
                            Id = 3,
                            GoodsReceiptId = 3,
                            ProductId = 1, // Bút bi Thiên Long
                            Quantity = 25,
                            UnitPrice = 4200
                        }
                    }
                }
            };
        }

        public async Task<IEnumerable<GoodsReceipt>> GetGoodsReceiptsAsync(ReceiptStatus? status = null)
        {
            IEnumerable<GoodsReceipt> query = _receipts;
            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            var list = query.OrderByDescending(r => r.ReceiptDate).ToList();
            foreach (var r in list)
            {
                r.Supplier = await _supplierRepository.GetSupplierByIdAsync(r.SupplierId);
                foreach (var line in r.Lines)
                {
                    line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                }
            }

            return list;
        }

        public async Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(int id)
        {
            var receipt = _receipts.FirstOrDefault(r => r.Id == id);
            if (receipt != null)
            {
                receipt.Supplier = await _supplierRepository.GetSupplierByIdAsync(receipt.SupplierId);
                foreach (var line in receipt.Lines)
                {
                    line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                }
            }
            return receipt;
        }

        public Task<int> CreateGoodsReceiptAsync(GoodsReceipt receipt)
        {
            lock (_receipts)
            {
                int maxId = _receipts.Any() ? _receipts.Max(r => r.Id) : 0;
                receipt.Id = maxId + 1;
                receipt.ReceiptNumber = $"PN-2026-{(receipt.Id):D3}";
                receipt.ReceiptDate = DateTime.Now;
                receipt.Status = ReceiptStatus.Draft;

                int lineId = 1;
                foreach (var line in receipt.Lines)
                {
                    line.Id = lineId++;
                    line.GoodsReceiptId = receipt.Id;
                }

                _receipts.Add(receipt);
                return Task.FromResult(receipt.Id);
            }
        }

        public async Task ApproveReceiptTransactionAsync(int receiptId, string approvedBy)
        {
            var receipt = _receipts.FirstOrDefault(r => r.Id == receiptId);
            if (receipt == null || receipt.Status != ReceiptStatus.Draft) return;

            // Transaction: Cập nhật Status & Tăng tồn kho
            lock (_receipts)
            {
                receipt.Status = ReceiptStatus.Approved;
                receipt.ApprovedBy = approvedBy;
                receipt.ApprovedDate = DateTime.Now;
            }

            foreach (var line in receipt.Lines)
            {
                await _productRepository.UpdateStockAsync(line.ProductId, line.Quantity);
            }
        }

        public Task CancelReceiptAsync(int receiptId)
        {
            var receipt = _receipts.FirstOrDefault(r => r.Id == receiptId);
            if (receipt != null && receipt.Status == ReceiptStatus.Draft)
            {
                receipt.Status = ReceiptStatus.Cancelled;
            }
            return Task.CompletedTask;
        }
    }
}
