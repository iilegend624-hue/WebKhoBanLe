using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class PosOrderLine
    {
        public int Id { get; set; }
        public int PosOrderId { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn sản phẩm hợp lệ")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mua phải lớn hơn 0")]
        public int Quantity { get; set; } = 1;

        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá bán không được âm")]
        public double UnitPrice { get; set; }

        public double LineTotal => Quantity * UnitPrice;
    }
}
