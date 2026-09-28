namespace KhoBanLe.CoreBusiness
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double LineTotal => Quantity * UnitPrice;
    }
}
