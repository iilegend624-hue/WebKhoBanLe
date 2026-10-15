using KhoBanLe.CoreBusiness;

namespace KhoBanLe.Services
{
    /// <summary>
    /// SLIDE 3: Quản lý trạng thái lập phiếu nháp (tương đương Shopping Cart trong Clean Architecture)
    /// </summary>
    public class DraftReceiptStateService
    {
        // Danh sách dòng phiếu nhập nháp
        public List<GoodsReceiptLine> DraftReceiptLines { get; private set; } = new();
        public int SelectedSupplierId { get; set; } = 1;
        public string ReceiptNote { get; set; } = string.Empty;

        // Danh sách dòng phiếu xuất nháp
        public List<GoodsIssueLine> DraftIssueLines { get; private set; } = new();
        public string CustomerOrDepartment { get; set; } = string.Empty;
        public string IssueNote { get; set; } = string.Empty;

        public event Action? OnChange;

        // Quản lý Phiếu Nhập
        public void AddReceiptLine(Product product, int quantity = 1, double? customPrice = null)
        {
            var existing = DraftReceiptLines.FirstOrDefault(l => l.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
                if (customPrice.HasValue) existing.UnitPrice = customPrice.Value;
            }
            else
            {
                DraftReceiptLines.Add(new GoodsReceiptLine
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = quantity,
                    UnitPrice = customPrice ?? product.Price
                });
            }
            NotifyStateChanged();
        }

        public void RemoveReceiptLine(int productId)
        {
            DraftReceiptLines.RemoveAll(l => l.ProductId == productId);
            NotifyStateChanged();
        }

        public void ClearReceipt()
        {
            DraftReceiptLines.Clear();
            ReceiptNote = string.Empty;
            NotifyStateChanged();
        }

        // Quản lý Phiếu Xuất
        public void AddIssueLine(Product product, int quantity = 1, double? customPrice = null)
        {
            var existing = DraftIssueLines.FirstOrDefault(l => l.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
                if (customPrice.HasValue) existing.UnitPrice = customPrice.Value;
            }
            else
            {
                DraftIssueLines.Add(new GoodsIssueLine
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = quantity,
                    UnitPrice = customPrice ?? product.Price
                });
            }
            NotifyStateChanged();
        }

        public void RemoveIssueLine(int productId)
        {
            DraftIssueLines.RemoveAll(l => l.ProductId == productId);
            NotifyStateChanged();
        }

        public void ClearIssue()
        {
            DraftIssueLines.Clear();
            CustomerOrDepartment = string.Empty;
            IssueNote = string.Empty;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
