using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã khách hàng là bắt buộc")]
        [StringLength(50)]
        public string CustomerCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public int Points { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
