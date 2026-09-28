using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class GoodsReceiptLine
    {
        public int Id { get; set; }
        public int GoodsReceiptId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn hàng hóa hợp lệ")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng nhập phải lớn hơn 0")]
        public int Quantity { get; set; } = 1;

        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá nhập không được âm")]
        public double UnitPrice { get; set; }

        public double LineTotal => Quantity * UnitPrice;
    }
}
