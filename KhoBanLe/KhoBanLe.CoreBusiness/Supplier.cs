using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã tối đa 50 ký tự")]
        public string SupplierCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên nhà cung cấp tối đa 200 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
