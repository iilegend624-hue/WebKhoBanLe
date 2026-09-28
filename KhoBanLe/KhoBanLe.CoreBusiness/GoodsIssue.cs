using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class GoodsIssue
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã phiếu xuất là bắt buộc")]
        public string IssueNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nơi nhận / Khách hàng / Bộ phận nhận là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tối đa 200 ký tự")]
        public string CustomerOrDepartment { get; set; } = string.Empty;

        public DateTime IssueDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Người lập phiếu là bắt buộc")]
        public string CreatedBy { get; set; } = "Thủ kho";

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public ReceiptStatus Status { get; set; } = ReceiptStatus.Draft;

        public string? Note { get; set; }

        public List<GoodsIssueLine> Lines { get; set; } = new();

        public int? PosOrderId { get; set; }
        public PosOrder? PosOrder { get; set; }

        public double TotalAmount => Lines?.Sum(l => l.LineTotal) ?? 0;
    }
}
