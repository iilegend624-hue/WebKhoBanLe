using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class GoodsReceipt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã phiếu nhập là bắt buộc")]
        public string ReceiptNumber { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }

        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Người lập phiếu là bắt buộc")]
        public string CreatedBy { get; set; } = "Thủ kho";

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public ReceiptStatus Status { get; set; } = ReceiptStatus.Draft;

        public string? Note { get; set; }

        public List<GoodsReceiptLine> Lines { get; set; } = new();

        public double TotalAmount => Lines?.Sum(l => l.LineTotal) ?? 0;
    }
}
