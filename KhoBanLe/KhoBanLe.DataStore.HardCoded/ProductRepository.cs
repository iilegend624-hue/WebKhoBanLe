using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Sku = "TL-01",
                    Name = "Bút bi Thiên Long TL-027 xanh",
                    Unit = "Cái",
                    Category = "Văn phòng phẩm",
                    Price = 5000,
                    CurrentStock = 5,      // Demo Luật 1: Tồn 5
                    MinStockLevel = 20,
                    Description = "Bút bi Thiên Long mực xanh cao cấp ngòi 0.5mm êm trơn.",
                    ImageUrl = "images/products/thienlong-tl027.jpg"
                },
                new Product
                {
                    Id = 2,
                    Sku = "HH-01",
                    Name = "Mì Hảo Hảo Tôm chua cay (thùng 30 gói)",
                    Unit = "Thùng",
                    Category = "Thực phẩm & Gia vị",
                    Price = 115000,
                    CurrentStock = 80,
                    MinStockLevel = 25,
                    Description = "Mì ăn liền Acecook Hảo Hảo vị Tôm chua cay truyền thống thùng 30 gói.",
                    ImageUrl = "images/products/haohao.jpg"
                },
                new Product
                {
                    Id = 3,
                    Sku = "VM-01",
                    Name = "Sữa tươi tiệt trùng Vinamilk 100% 1L",
                    Unit = "Hộp",
                    Category = "Sữa & Chế phẩm sữa",
                    Price = 34000,
                    CurrentStock = 35,
                    MinStockLevel = 15,
                    Description = "Sữa tươi tiệt trùng Vinamilk có đường 1 lít giàu canxi và vitamin D3.",
                    ImageUrl = "images/products/vinamilk.jpg"
                },
                new Product
                {
                    Id = 4,
                    Sku = "CC-01",
                    Name = "Nước ngọt Coca-Cola lon 320ml (thùng 24 lon)",
                    Unit = "Thùng",
                    Category = "Nước giải khát & Bia",
                    Price = 210000,
                    CurrentStock = 50,
                    MinStockLevel = 20,
                    Description = "Nước giải khát có ga Coca-Cola sảng khoái nguyên thùng 24 lon.",
                    ImageUrl = "images/products/coca-cola.png"
                },
                new Product
                {
                    Id = 5,
                    Sku = "ST25-05",
                    Name = "Gạo thơm Sóc Trăng ST25 thượng hạng 5kg",
                    Unit = "Túi",
                    Category = "Thực phẩm & Gia vị",
                    Price = 185000,
                    CurrentStock = 40,
                    MinStockLevel = 10,
                    Description = "Gạo ST25 gạo ngon nhất thế giới, hạt dài, dẻo thơm hảo hạng.",
                    ImageUrl = "images/products/st25-5kg.jpg"
                },
                new Product
                {
                    Id = 6,
                    Sku = "SP-01",
                    Name = "Dầu ăn đậu nành Simply nguyên chất 1L",
                    Unit = "Chai",
                    Category = "Thực phẩm & Gia vị",
                    Price = 58000,
                    CurrentStock = 30,
                    MinStockLevel = 10,
                    Description = "Dầu đậu nành nguyên chất 100% tốt cho tim mạch chai 1L.",
                    ImageUrl = "images/products/simply-1l.jpg"
                },
                new Product
                {
                    Id = 7,
                    Sku = "SL-01",
                    Name = "Nước rửa chén Sunlight Chanh túi 750g",
                    Unit = "Túi",
                    Category = "Hóa mỹ phẩm & Đồ gia dụng",
                    Price = 32000,
                    CurrentStock = 6,      // Sắp hết hàng (6 < 15)
                    MinStockLevel = 15,
                    Description = "Nước rửa chén Sunlight tinh chất chanh tươi sạch dầu mỡ.",
                    ImageUrl = "images/products/sunlight-chanh.jpg"
                },
                new Product
                {
                    Id = 8,
                    Sku = "PP-01",
                    Name = "Khăn giấy lụa cao cấp Pulppy 10 cuộn",
                    Unit = "Lốc",
                    Category = "Hóa mỹ phẩm & Đồ gia dụng",
                    Price = 85000,
                    CurrentStock = 0,      // Hết hàng (0)
                    MinStockLevel = 10,
                    Description = "Khăn giấy lụa Pulppy mềm mại an toàn, thấm hút vượt trội.",
                    ImageUrl = "images/products/pulppy-10cuon.jpg"
                },
                new Product
                {
                    Id = 9,
                    Sku = "CC-LON",
                    Name = "Nước ngọt Coca-Cola vị nguyên bản lon 320ml",
                    Unit = "Lon",
                    Category = "Nước giải khát & Bia",
                    Price = 11000,
                    CurrentStock = 150,
                    MinStockLevel = 24,
                    Description = "Nước giải khát có ga Coca-Cola sảng khoái lon lẻ 320ml bán chạy tại quầy.",
                    ImageUrl = "images/products/coca-cola.png"
                },
                new Product
                {
                    Id = 10,
                    Sku = "RB-01",
                    Name = "Nước tăng lực Red Bull Thái lon 250ml",
                    Unit = "Lon",
                    Category = "Nước giải khát & Bia",
                    Price = 15000,
                    CurrentStock = 120,
                    MinStockLevel = 24,
                    Description = "Nước tăng lực bò húc Red Bull nắp vàng Thái Lan chính hãng.",
                    ImageUrl = "images/products/redbull.jpg"
                },
                new Product
                {
                    Id = 11,
                    Sku = "RM-650",
                    Name = "Dầu gội nam Romano Classic sạch gàu 650g",
                    Unit = "Chai",
                    Category = "Hóa mỹ phẩm & Đồ gia dụng",
                    Price = 175000,
                    CurrentStock = 45,
                    MinStockLevel = 10,
                    Description = "Dầu gội nước hoa cao cấp Romano hương Classic lịch lãm, sạch gàu.",
                    ImageUrl = "images/products/romano-classic.jpg"
                },
                new Product
                {
                    Id = 12,
                    Sku = "LAY-01",
                    Name = "Snack khoai tây Lay's vị tự nhiên Classic 95g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 22000,
                    CurrentStock = 85,
                    MinStockLevel = 15,
                    Description = "Bim bim khoai tây chiên vàng giòn rụm vị muối tự nhiên thơm ngon.",
                    ImageUrl = "images/products/lays.jpg"
                },
                new Product
                {
                    Id = 13,
                    Sku = "CP-12",
                    Name = "Bánh ChocoPie Orion hộp 12 cái (396g)",
                    Unit = "Hộp",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 56000,
                    CurrentStock = 60,
                    MinStockLevel = 10,
                    Description = "Bánh sôcôla ChocoPie kẹp kem dẻo marshmallow thơm lừng ngọt ngào.",
                    ImageUrl = "images/products/chocopie-orion.jpg"
                },
                new Product
                {
                    Id = 14,
                    Sku = "LV-500",
                    Name = "Nước khoáng thiên nhiên LaVie chai 500ml",
                    Unit = "Chai",
                    Category = "Nước giải khát & Bia",
                    Price = 6000,
                    CurrentStock = 200,
                    MinStockLevel = 48,
                    Description = "Nước khoáng LaVie đóng chai tự nhiên thanh khiết cho sức khỏe.",
                    ImageUrl = "images/products/lavie-500ml.jpg"
                },
                new Product
                {
                    Id = 15,
                    Sku = "HN-330",
                    Name = "Bia Heineken Silver lon 330ml",
                    Unit = "Lon",
                    Category = "Nước giải khát & Bia",
                    Price = 21000,
                    CurrentStock = 95,
                    MinStockLevel = 24,
                    Description = "Bia Heineken Silver nhẹ êm dễ uống, hương vị tươi mát sảng khoái.",
                    ImageUrl = "images/products/heineken.png"
                },
                new Product
                {
                    Id = 16,
                    Sku = "VM-LOC",
                    Name = "Sữa tươi Vinamilk có đường 180ml (lốc 4 hộp)",
                    Unit = "Lốc",
                    Category = "Sữa & Chế phẩm sữa",
                    Price = 34000,
                    CurrentStock = 80,
                    MinStockLevel = 20,
                    Description = "Lốc 4 hộp sữa tươi tiệt trùng Vinamilk có đường giàu dinh dưỡng.",
                    ImageUrl = "images/products/vinamilk.jpg"
                },
                new Product
                {
                    Id = 17,
                    Sku = "SC-VM",
                    Name = "Sữa chua ăn Vinamilk có đường (lốc 4 hộp x 100g)",
                    Unit = "Lốc",
                    Category = "Sữa & Chế phẩm sữa",
                    Price = 28000,
                    CurrentStock = 70,
                    MinStockLevel = 15,
                    Description = "Sữa chua lên men tự nhiên bổ sung men vi sinh tốt cho hệ tiêu hóa.",
                    ImageUrl = "images/products/vinamilk-yogurt.jpg"
                },
                new Product
                {
                    Id = 18,
                    Sku = "OT-380",
                    Name = "Sữa đặc có đường Ông Thọ đỏ lon 380g",
                    Unit = "Lon",
                    Category = "Sữa & Chế phẩm sữa",
                    Price = 26000,
                    CurrentStock = 90,
                    MinStockLevel = 20,
                    Description = "Sữa đặc Ông Thọ sánh đặc ngọt béo dùng pha cà phê sữa thơm ngon.",
                    ImageUrl = "images/products/ongtho-do.jpg"
                },
                new Product
                {
                    Id = 19,
                    Sku = "HH-GOI",
                    Name = "Mì Hảo Hảo tôm chua cay gói 75g",
                    Unit = "Gói",
                    Category = "Thực phẩm & Gia vị",
                    Price = 4500,
                    CurrentStock = 300,
                    MinStockLevel = 50,
                    Description = "Mì gói Hảo Hảo tôm chua cay bán lẻ từng gói, món ăn quốc dân.",
                    ImageUrl = "images/products/haohao.jpg"
                },
                new Product
                {
                    Id = 20,
                    Sku = "NN-900",
                    Name = "Nước mắm Nam Ngư Đệ Nhị chai 900ml",
                    Unit = "Chai",
                    Category = "Thực phẩm & Gia vị",
                    Price = 32000,
                    CurrentStock = 65,
                    MinStockLevel = 15,
                    Description = "Nước mắm Nam Ngư Đệ Nhị vị ngon đậm đà, chấm hay nấu đều thơm.",
                    ImageUrl = "images/products/namngu.jpg"
                },
                new Product
                {
                    Id = 21,
                    Sku = "KN-400",
                    Name = "Hạt nêm Knorr thịt thăn xương ống 400g",
                    Unit = "Gói",
                    Category = "Thực phẩm & Gia vị",
                    Price = 39000,
                    CurrentStock = 55,
                    MinStockLevel = 15,
                    Description = "Hạt nêm Knorr chiết xuất thịt thăn xương ống cho canh ngọt thanh đậm đà.",
                    ImageUrl = "images/products/knorr.jpg"
                },
                new Product
                {
                    Id = 22,
                    Sku = "CS-250",
                    Name = "Tương ớt Chinsu chai 250g",
                    Unit = "Chai",
                    Category = "Thực phẩm & Gia vị",
                    Price = 14000,
                    CurrentStock = 70,
                    MinStockLevel = 20,
                    Description = "Tương ớt Chinsu thơm cay bùng nổ hương vị chấm món gì cũng ngon.",
                    ImageUrl = "images/products/chinsu.jpg"
                },
                new Product
                {
                    Id = 23,
                    Sku = "CG-180",
                    Name = "Kem đánh răng Colgate Total than hoạt tính 180g",
                    Unit = "Hộp",
                    Category = "Hóa mỹ phẩm & Đồ gia dụng",
                    Price = 45000,
                    CurrentStock = 50,
                    MinStockLevel = 15,
                    Description = "Kem đánh răng Colgate bảo vệ răng miệng toàn diện suốt 12h.",
                    ImageUrl = "images/products/colgate.jpg"
                },
                new Product
                {
                    Id = 24,
                    Sku = "OM-2KG",
                    Name = "Bột giặt Omo Matic cửa trên túi 2kg",
                    Unit = "Túi",
                    Category = "Hóa mỹ phẩm & Đồ gia dụng",
                    Price = 135000,
                    CurrentStock = 30,
                    MinStockLevel = 10,
                    Description = "Bột giặt Omo Matic xoáy bay vết bẩn cứng đầu, thơm lâu ngát hương.",
                    ImageUrl = "images/products/omo.jpg"
                },
                new Product
                {
                    Id = 25,
                    Sku = "CF-18",
                    Name = "Nước xả vải Comfort một lần xả túi 1.8L",
                    Unit = "Túi",
                    Category = "Hóa mỹ phẩm & Đồ gia dụng",
                    Price = 118000,
                    CurrentStock = 40,
                    MinStockLevel = 10,
                    Description = "Nước xả vải Comfort hương ban mai bền lâu giúp quần áo mềm mại.",
                    ImageUrl = "images/products/comfort-18l.jpg"
                },
                new Product
                {
                    Id = 26,
                    Sku = "CC-PD",
                    Name = "Kẹo dẻo Chupa Chups Panda vị trái cây 90g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 16000,
                    CurrentStock = 100,
                    MinStockLevel = 20,
                    Description = "Kẹo dẻo hình gấu Panda hương trái cây thơm ngon dai mềm vui nhộn.",
                    ImageUrl = "images/products/chupachups-panda.jpg"
                },
                new Product
                {
                    Id = 27,
                    Sku = "CS-288",
                    Name = "Bánh quy Cosy Marie Kinh Đô 288g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 38000,
                    CurrentStock = 45,
                    MinStockLevel = 10,
                    Description = "Bánh quy sữa Cosy Marie giòn xốp béo ngậy vị sữa tươi nguyên chất.",
                    ImageUrl = "images/products/cosy-marie.jpg"
                },
                new Product
                {
                    Id = 28,
                    Sku = "CP-A5",
                    Name = "Sổ tay Campus A5 kẻ ngang 200 trang",
                    Unit = "Quyển",
                    Category = "Văn phòng phẩm",
                    Price = 25000,
                    CurrentStock = 60,
                    MinStockLevel = 15,
                    Description = "Sổ tay lò xo bìa cứng Campus giấy chống lóa viết êm không thấm mực.",
                    ImageUrl = "images/products/campus-a5.jpg"
                },
                new Product
                {
                    Id = 29,
                    Sku = "C2-01",
                    Name = "Trà xanh C2 hương chanh chai 455ml",
                    Unit = "Chai",
                    Category = "Nước giải khát & Bia",
                    Price = 10000,
                    CurrentStock = 85,
                    MinStockLevel = 24,
                    Description = "Trà xanh đóng chai C2 ủ từ lá trà Thái Nguyên vị chanh thanh khiết.",
                    ImageUrl = "images/products/c2-lemon.jpg"
                },
                new Product
                {
                    Id = 30,
                    Sku = "SW-NY",
                    Name = "Snack khoai tây Swing vị Bít tết New York 63g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 12500,
                    CurrentStock = 65,
                    MinStockLevel = 15,
                    Description = "Snack khoai tây lát gợn sóng Swing vị Bít tết New York đậm đà giòn rụm.",
                    ImageUrl = "images/products/swing-steak.jpg"
                },
                new Product
                {
                    Id = 31,
                    Sku = "SW-GA",
                    Name = "Snack khoai tây Swing vị Cánh gà nướng mật ong 63g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 12500,
                    CurrentStock = 50,
                    MinStockLevel = 15,
                    Description = "Snack khoai tây Swing cánh gà nướng thơm lừng vị mật ong hấp dẫn.",
                    ImageUrl = "images/products/swing-chicken.jpg"
                },
                new Product
                {
                    Id = 32,
                    Sku = "OST-TN",
                    Name = "Snack khoai tây O'Star vị tự nhiên muối biển 63g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 12000,
                    CurrentStock = 80,
                    MinStockLevel = 20,
                    Description = "Snack khoai tây O'Star vị muối biển tự nhiên giòn tan thơm bùi đặc trưng.",
                    ImageUrl = "images/products/ostar-salt.jpg"
                },
                new Product
                {
                    Id = 33,
                    Sku = "OST-KC",
                    Name = "Snack khoai tây O'Star vị Kim chi Hàn Quốc 63g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 12000,
                    CurrentStock = 75,
                    MinStockLevel = 20,
                    Description = "Snack khoai tây lát O'Star vị Kim chi cay nhẹ chua ngọt đậm đà chuẩn vị Hàn.",
                    ImageUrl = "images/products/ostar-kimchi.jpg"
                },
                new Product
                {
                    Id = 34,
                    Sku = "OST-TB",
                    Name = "Snack khoai tây O'Star vị Tảo biển 63g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 12000,
                    CurrentStock = 60,
                    MinStockLevel = 15,
                    Description = "Snack khoai tây O'Star rắc tảo biển thơm lừng giòn tan vui miệng.",
                    ImageUrl = "images/products/ostar-seaweed.jpg"
                },
                new Product
                {
                    Id = 35,
                    Sku = "TOO-PM",
                    Name = "Snack que Toonies vị Phô mai bùng nổ 65g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 7000,
                    CurrentStock = 90,
                    MinStockLevel = 20,
                    Description = "Snack que Toonies giòn rụm ngập tràn sốt phô mai béo ngậy món ăn vặt tuổi thơ.",
                    ImageUrl = "images/products/toonies.jpg"
                },
                new Product
                {
                    Id = 36,
                    Sku = "CC-MUC",
                    Name = "Snack mực lăn sa tế Chop Chop 50g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 7000,
                    CurrentStock = 85,
                    MinStockLevel = 20,
                    Description = "Snack mực nướng sa tế Chop Chop giòn cay kích thích vị giác cực kỳ lôi cuốn.",
                    ImageUrl = "images/products/chopchop.jpg"
                },
                new Product
                {
                    Id = 37,
                    Sku = "TY-X",
                    Name = "Snack que TaYo! X vị Tảo ngọt 60g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 8000,
                    CurrentStock = 70,
                    MinStockLevel = 15,
                    Description = "Snack que rỗng TaYo! X vị tảo ngọt giòn xốp hương vị mới lạ hấp dẫn.",
                    ImageUrl = "images/products/tayo-x.jpg"
                },
                new Product
                {
                    Id = 38,
                    Sku = "TY-TBK",
                    Name = "Snack bánh gạo TaYo! vị cay Tokbokki 60g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 8000,
                    CurrentStock = 65,
                    MinStockLevel = 15,
                    Description = "Bánh gạo lắc vị Tokbokki Hàn Quốc cay ngọt giòn tan đậm đà.",
                    ImageUrl = "images/products/tayo-tokbokki.jpg"
                },
                new Product
                {
                    Id = 39,
                    Sku = "CORN-01",
                    Name = "Snack bắp nướng bơ Corn Chip 56g",
                    Unit = "Gói",
                    Category = "Bánh kẹo & Đồ ăn vặt",
                    Price = 7500,
                    CurrentStock = 70,
                    MinStockLevel = 15,
                    Description = "Snack bắp nướng bơ sữa thơm ngậy giòn tan hương vị bắp ngọt tự nhiên.",
                    ImageUrl = "images/products/cornchip.jpg"
                },
                new Product
                {
                    Id = 40,
                    Sku = "STING-01",
                    Name = "Nước tăng lực Sting hương dâu tây lon 320ml",
                    Unit = "Lon",
                    Category = "Nước giải khát & Bia",
                    Price = 11000,
                    CurrentStock = 120,
                    MinStockLevel = 24,
                    Description = "Nước tăng lực Sting dâu đỏ lon cao sảng khoái bổ sung năng lượng tức thì.",
                    ImageUrl = "images/products/sting.jpg"
                },
                new Product
                {
                    Id = 41,
                    Sku = "OM-BO",
                    Name = "Mì khoai tây Omachi xốt bò hầm gói 82g",
                    Unit = "Gói",
                    Category = "Thực phẩm & Gia vị",
                    Price = 9000,
                    CurrentStock = 150,
                    MinStockLevel = 30,
                    Description = "Mì khoai tây Omachi sợi mì vàng dai mướt kết hợp xốt bò hầm thơm ngon không lo bị nóng.",
                    ImageUrl = "images/products/omachi.jpg"
                }
            };
        }

        public Task<IEnumerable<Product>> GetProductsAsync(string? filter = null, string? category = null)
        {
            IEnumerable<Product> query = _products;

            if (!string.IsNullOrWhiteSpace(category) && category != "Tất cả")
            {
                query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p =>
                    p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                    p.Sku.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            return Task.FromResult(query.OrderBy(p => p.Id).AsEnumerable());
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<Product?> GetProductBySkuAsync(string sku)
        {
            var product = _products.FirstOrDefault(p => p.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(product);
        }

        public Task AddProductAsync(Product product)
        {
            lock (_products)
            {
                int maxId = _products.Any() ? _products.Max(p => p.Id) : 0;
                product.Id = maxId + 1;
                _products.Add(product);
            }
            return Task.CompletedTask;
        }

        public Task UpdateProductAsync(Product product)
        {
            lock (_products)
            {
                var existing = _products.FirstOrDefault(p => p.Id == product.Id);
                if (existing != null)
                {
                    existing.Sku = product.Sku;
                    existing.Name = product.Name;
                    existing.Unit = product.Unit;
                    existing.Category = product.Category;
                    existing.Price = product.Price;
                    existing.MinStockLevel = product.MinStockLevel;
                    existing.Description = product.Description;
                    existing.ImageUrl = product.ImageUrl;
                }
            }
            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            lock (_products)
            {
                _products.RemoveAll(p => p.Id == id);
            }
            return Task.CompletedTask;
        }

        public Task UpdateStockAsync(int productId, int quantityChange)
        {
            lock (_products)
            {
                var existing = _products.FirstOrDefault(p => p.Id == productId);
                if (existing != null)
                {
                    existing.CurrentStock += quantityChange;
                    if (existing.CurrentStock < 0) existing.CurrentStock = 0;
                }
            }
            return Task.CompletedTask;
        }
    }
}
