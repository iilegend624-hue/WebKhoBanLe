using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class PosOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã hóa đơn POS là bắt buộc")]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(200)]
        public string CustomerName { get; set; } = "Khách mua lẻ tại quầy";

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Tên thu ngân là bắt buộc")]
        [StringLength(100)]
        public string CashierName { get; set; } = "Thu ngân / Bán lẻ";

        [Required(ErrorMessage = "Phương thức thanh toán là bắt buộc")]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Tiền mặt"; // Tiền mặt, Chuyển khoản QR, Quẹt thẻ POS

        public double SubTotal { get; set; }

        public double DiscountAmount { get; set; } = 0;

        public double TotalAmount { get; set; }

        public double ReceivedAmount { get; set; } = 0; // Tiền khách đưa

        public double ChangeAmount { get; set; } = 0;   // Tiền thối lại

        public int? GoodsIssueId { get; set; }          // Khóa ngoại liên kết tới Phiếu xuất kho
        public GoodsIssue? GoodsIssue { get; set; }

        public string Status { get; set; } = "Paid";    // Paid (Đã thanh toán), Cancelled (Đã hủy)

        public string? Note { get; set; }

        public List<PosOrderLine> Lines { get; set; } = new();

        public int TotalItems => Lines?.Sum(l => l.Quantity) ?? 0;
    }
}
