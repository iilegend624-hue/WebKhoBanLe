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
                    Name = "Bút bi Thiên Long TL-027",
                    Unit = "Cái",
                    Category = "Văn phòng phẩm",
                    Price = 5000,
                    CurrentStock = 5,      // ĐÚNG THEO SLIDE 7 ĐỂ DEMO LUẬT 1: Tồn 5, yêu cầu xuất 10 -> Chặn lỗi!
                    MinStockLevel = 20,
                    Description = "Bút bi Thiên Long mực xanh cao cấp ngòi 0.5mm êm trơn.",
                    ImageUrl = "https://images.unsplash.com/photo-1583485088034-697b5bc54ccd?w=300"
                },
                new Product
                {
                    Id = 2,
                    Sku = "HH-01",
                    Name = "Mì Hảo Hảo Tôm chua cay (30 gói)",
                    Unit = "Thùng",
                    Category = "Thực phẩm",
                    Price = 115000,
                    CurrentStock = 80,
                    MinStockLevel = 25,
                    Description = "Mì ăn liền Acecook Hảo Hảo vị Tôm chua cay truyền thống.",
                    ImageUrl = "https://images.unsplash.com/photo-1612927601601-6638404737ce?w=300"
                },
                new Product
                {
                    Id = 3,
                    Sku = "VM-01",
                    Name = "Sữa tươi tiệt trùng Vinamilk 100% 1L",
                    Unit = "Hộp",
                    Category = "Sữa & Đồ uống",
                    Price = 34000,
                    CurrentStock = 35,
                    MinStockLevel = 15,
                    Description = "Sữa tươi tiệt trùng Vinamilk có đường 1 lít giàu canxi và vitamin.",
                    ImageUrl = "https://images.unsplash.com/photo-1550583724-b2692b85b150?w=300"
                },
                new Product
                {
                    Id = 4,
                    Sku = "CC-01",
                    Name = "Nước ngọt Coca-Cola lon 330ml (24 lon)",
                    Unit = "Thùng",
                    Category = "Sữa & Đồ uống",
                    Price = 210000,
                    CurrentStock = 50,
                    MinStockLevel = 20,
                    Description = "Nước giải khát có ga Coca-Cola sảng khoái.",
                    ImageUrl = "https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=300"
                },
                new Product
                {
                    Id = 5,
                    Sku = "ST25-05",
                    Name = "Gạo thơm Sóc Trăng ST25 thượng hạng 5kg",
                    Unit = "Túi",
                    Category = "Thực phẩm",
                    Price = 185000,
                    CurrentStock = 40,
                    MinStockLevel = 10,
                    Description = "Gạo ST25 gạo ngon nhất thế giới, hạt dài, dẻo thơm.",
                    ImageUrl = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300"
                },
                new Product
                {
                    Id = 6,
                    Sku = "SP-01",
                    Name = "Dầu ăn đậu nành Simply nguyên chất 1L",
                    Unit = "Chai",
                    Category = "Thực phẩm",
                    Price = 58000,
                    CurrentStock = 30,
                    MinStockLevel = 10,
                    Description = "Dầu đậu nành nguyên chất 100% tốt cho tim mạch.",
                    ImageUrl = "https://images.unsplash.com/photo-1474979266404-7eaacbcd87c5?w=300"
                },
                new Product
                {
                    Id = 7,
                    Sku = "SL-01",
                    Name = "Nước rửa chén Sunlight Chanh túi 750g",
                    Unit = "Túi",
                    Category = "Hóa mỹ phẩm",
                    Price = 32000,
                    CurrentStock = 6,      // Sắp hết hàng (6 < 15) -> Badge màu Vàng
                    MinStockLevel = 15,
                    Description = "Nước rửa chén Sunlight tinh chất chanh sạch dầu mỡ.",
                    ImageUrl = "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?w=300"
                },
                new Product
                {
                    Id = 8,
                    Sku = "PP-01",
                    Name = "Khăn giấy lụa cao cấp Pulppy 10 cuộn",
                    Unit = "Lốc",
                    Category = "Đồ gia dụng - Sinh hoạt",
                    Price = 85000,
                    CurrentStock = 0,      // Hết hàng (0) -> Badge màu Đỏ
                    MinStockLevel = 10,
                    Description = "Khăn giấy Pulppy mềm mại, thấm hút tốt, an toàn cho da.",
                    ImageUrl = "https://images.unsplash.com/photo-1584556812952-905ffd0c611a?w=400"
                },
                // --- CÁC MẪU GIAO DIỆN & MẶT HÀNG CHUẨN THEO ẢNH CHỤP MÀN HÌNH SAPO ---
                new Product
                {
                    Id = 9,
                    Sku = "EGO-HELIOS",
                    Name = "Ego Helios",
                    Unit = "Gói",
                    Category = "Thời trang - Trang sức",
                    Price = 1800000,
                    OriginalPrice = 2200000,
                    CurrentStock = 45,
                    MinStockLevel = 10,
                    Description = "Giao diện web bán hàng Thời trang & Đồng hồ cao cấp, phong cách sang trọng hiện đại.",
                    ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=600"
                },
                new Product
                {
                    Id = 10,
                    Sku = "EGO-FITNESS",
                    Name = "Ego Fitness",
                    Unit = "Gói",
                    Category = "Thể thao - Dịch vụ",
                    Price = 1900000,
                    OriginalPrice = null,
                    CurrentStock = 32,
                    MinStockLevel = 8,
                    Description = "Giao diện thể thao, phòng gym, thực phẩm thể hình năng động và bứt phá doanh số.",
                    ImageUrl = "https://images.unsplash.com/photo-1517838277536-f5f99be501cd?w=600"
                },
                new Product
                {
                    Id = 11,
                    Sku = "HER-COSMETIC",
                    Name = "Her Cosmetic",
                    Unit = "Gói",
                    Category = "Mỹ phẩm - Làm đẹp",
                    Price = 1840000,
                    OriginalPrice = 2300000,
                    CurrentStock = 50,
                    MinStockLevel = 12,
                    Description = "Giao diện mỹ phẩm, làm đẹp, spa sắc sảo và thanh lịch tối ưu tỷ lệ chuyển đổi.",
                    ImageUrl = "https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600"
                },
                new Product
                {
                    Id = 12,
                    Sku = "ORGANIC-FOOD",
                    Name = "Thực Phẩm Hữu Cơ Giàu Dinh Dưỡng",
                    Unit = "Combo",
                    Category = "Thực phẩm - Nhà hàng",
                    Price = 1200000,
                    OriginalPrice = 1500000,
                    CurrentStock = 28,
                    MinStockLevel = 10,
                    Description = "Giao diện cửa hàng thực phẩm sạch hữu cơ, rau củ quả tươi sống, chuẩn SEO.",
                    ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600"
                },
                new Product
                {
                    Id = 13,
                    Sku = "COZY-HOME",
                    Name = "Nội Thất Cozy Home & Decor",
                    Unit = "Gói",
                    Category = "Nội thất - Trang trí",
                    Price = 1500000,
                    OriginalPrice = 1800000,
                    CurrentStock = 20,
                    MinStockLevel = 5,
                    Description = "Mẫu giao diện nội thất gia đình, kiến trúc và trang trí nhà cửa cao cấp.",
                    ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=600"
                },
                new Product
                {
                    Id = 14,
                    Sku = "SMART-TECH",
                    Name = "SmartTech Điện Tử Số",
                    Unit = "Gói",
                    Category = "Công nghệ - Kỹ thuật số",
                    Price = 2100000,
                    OriginalPrice = 2500000,
                    CurrentStock = 15,
                    MinStockLevel = 5,
                    Description = "Cửa hàng điện thoại, máy tính, linh kiện điện tử hiện đại và mạnh mẽ.",
                    ImageUrl = "https://images.unsplash.com/photo-1519389950473-47ba0277781c?w=600"
                },
                new Product
                {
                    Id = 15,
                    Sku = "NANO-LIGHT",
                    Name = "Thiết Bị Điện Gia Đình NanoLight",
                    Unit = "Gói",
                    Category = "Thiết bị điện",
                    Price = 1450000,
                    OriginalPrice = 1700000,
                    CurrentStock = 25,
                    MinStockLevel = 8,
                    Description = "Thiết bị chiếu sáng, điện dân dụng, giải pháp điện thông minh.",
                    ImageUrl = "https://images.unsplash.com/photo-1558002038-1055907df827?w=600"
                },
                new Product
                {
                    Id = 16,
                    Sku = "AUTO-PRO",
                    Name = "Phụ Kiện Ô Tô - Xe Máy Sport",
                    Unit = "Gói",
                    Category = "Ô tô - Xe máy",
                    Price = 2500000,
                    OriginalPrice = null,
                    CurrentStock = 18,
                    MinStockLevel = 5,
                    Description = "Giao diện gara, đồ chơi ô tô xe máy, phụ tùng xe phân khối lớn.",
                    ImageUrl = "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=600"
                },
                new Product
                {
                    Id = 17,
                    Sku = "CRAFT-GIFT",
                    Name = "Thủ Công Mỹ Nghệ & Quà Tặng Tinh Xảo",
                    Unit = "Gói",
                    Category = "Thủ công - Mỹ nghệ - Quà tặng",
                    Price = 680000,
                    OriginalPrice = 850000,
                    CurrentStock = 30,
                    MinStockLevel = 10,
                    Description = "Đồ lưu niệm, gốm sứ Bát Tràng, tranh thêu và quà tặng thủ công truyền thống.",
                    ImageUrl = "https://images.unsplash.com/photo-1513519245088-0e12902e5a38?w=600"
                },
                new Product
                {
                    Id = 18,
                    Sku = "BLUE-SKY",
                    Name = "Khách Sạn & Du Lịch BlueSky Resort",
                    Unit = "Gói",
                    Category = "Khách sạn - Du lịch",
                    Price = 2200000,
                    OriginalPrice = 2700000,
                    CurrentStock = 12,
                    MinStockLevel = 4,
                    Description = "Đặt phòng khách sạn, tour du lịch, nghỉ dưỡng và vé máy bay trực tuyến.",
                    ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=600"
                },
                new Product
                {
                    Id = 19,
                    Sku = "PRIME-LAND",
                    Name = "Bất Động Sản Đất Vàng Prime Land",
                    Unit = "Gói",
                    Category = "Bất động sản - Xây dựng",
                    Price = 2800000,
                    OriginalPrice = 3200000,
                    CurrentStock = 10,
                    MinStockLevel = 3,
                    Description = "Dự án căn hộ, biệt thự nghỉ dưỡng, sàn giao dịch bất động sản chuyên nghiệp.",
                    ImageUrl = "https://images.unsplash.com/photo-1560518883-ce09059eeffa?w=600"
                },
                new Product
                {
                    Id = 20,
                    Sku = "PRO-BIZ",
                    Name = "Hồ Sơ Doanh Nghiệp ProBiz",
                    Unit = "Gói",
                    Category = "Doanh nghiệp",
                    Price = 1950000,
                    OriginalPrice = null,
                    CurrentStock = 22,
                    MinStockLevel = 6,
                    Description = "Trang giới thiệu công ty, tập đoàn đa ngành, hồ sơ năng lực uy tín.",
                    ImageUrl = "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=600"
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
