using System.ComponentModel.DataAnnotations;

namespace KhoBanLe.CoreBusiness
{
    public class GoodsIssueLine
    {
        public int Id { get; set; }
        public int GoodsIssueId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn hàng hóa hợp lệ")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng xuất phải lớn hơn 0")]
        public int Quantity { get; set; } = 1;

        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá xuất không được âm")]
        public double UnitPrice { get; set; }

        public double LineTotal => Quantity * UnitPrice;
    }
}
