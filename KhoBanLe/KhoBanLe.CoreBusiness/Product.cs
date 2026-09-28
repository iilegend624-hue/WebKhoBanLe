using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã hàng (SKU) là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã SKU tối đa 50 ký tự")]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên hàng hóa là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên hàng hóa tối đa 200 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Đơn vị tính là bắt buộc")]
        [StringLength(50, ErrorMessage = "ĐVT tối đa 50 ký tự")]
        public string Unit { get; set; } = "Cái";

        public string Category { get; set; } = "Bách hóa tổng hợp";

        [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn hoặc bằng 0")]
        public double Price { get; set; }

        public double? OriginalPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho hiện tại không được âm")]
        public int CurrentStock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Định mức tồn tối thiểu không được âm")]
        public int MinStockLevel { get; set; } = 10;

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
