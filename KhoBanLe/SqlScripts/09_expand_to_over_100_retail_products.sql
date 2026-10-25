-- =========================================================================================
-- Script 09: Bổ sung hơn 40 mặt hàng bán lẻ thiết yếu chuẩn thị trường Việt Nam
-- Đạt tổng cộng hơn 110 sản phẩm thuộc 10 ngành hàng bán lẻ tiêu dùng
-- Idempotent: Kiểm tra IF NOT EXISTS theo Sku, an toàn khi chạy nhiều lần
-- =========================================================================================

USE [KhoBanLe];
GO

SET NOCOUNT ON;

-- 1. NƯỚC GIẢI KHÁT & BIA
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'STR-330')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'STR-330', N'Nước tăng lực Sting hương dâu tây đỏ lon 320ml', N'Lon', N'Nước giải khát & Bia', 11000, 65, 24, N'Nước tăng lực có ga Sting đỏ hương dâu tây dồi dào năng lượng và Vitamin B.', N'images/products/sting.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'RB-250')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'RB-250', N'Nước tăng lực Red Bull nắp vàng lon 250ml', N'Lon', N'Nước giải khát & Bia', 14000, 80, 20, N'Nước tăng lực Red Bull bò húc Thái Lan bổ sung taurine hồi phục sức khỏe nhanh chóng.', N'images/products/redbull.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'C2-360')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'C2-360', N'Trà xanh C2 hương chanh tươi thanh mát chai 360ml', N'Chai', N'Nước giải khát & Bia', 8500, 50, 15, N'Trà xanh C2 chắt lọc từ 100% lá trà tự nhiên cao nguyên thanh nhiệt sảng khoái.', N'images/products/c2-lemon.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'LV-500')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'LV-500', N'Nước khoáng thiên nhiên La Vie chai 500ml', N'Chai', N'Nước giải khát & Bia', 6000, 120, 30, N'Nước khoáng La Vie nguồn khoáng ngầm dịu nhẹ cân bằng khoáng chất cơ thể mỗi ngày.', N'images/products/lavie-500ml.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HN-330')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HN-330', N'Bia Heineken Silver lon cao 330ml', N'Lon', N'Nước giải khát & Bia', 21000, 48, 24, N'Bia Heineken Silver hương vị nhẹ êm dễ uống lưu giữ trọn vẹn chất lượng thượng hạng.', N'images/products/heineken.jpg', SYSDATETIME());

-- 2. SỮA & CHẾ PHẨM SỮA
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'VM-SC100')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'VM-SC100', N'Sữa chua ăn Vinamilk có đường hộp 100g', N'Hộp', N'Sữa & Chế phẩm sữa', 7500, 60, 20, N'Sữa chua lên men tự nhiên giàu canxi và lợi khuẩn tốt cho hệ tiêu hóa gia đình.', N'images/products/vinamilk-yogurt.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OT-380')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'OT-380', N'Sữa đặc có đường Ông Thọ nhãn đỏ lon 380g', N'Lon', N'Sữa & Chế phẩm sữa', 26500, 45, 12, N'Sữa đặc Ông Thọ sánh đặc béo ngậy pha cà phê sữa thơm nồng đậm đà chuẩn vị truyền thống.', N'images/products/ongtho-do.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TH-1L')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'TH-1L', N'Sữa tươi tiệt trùng nguyên chất TH true MILK 1L', N'Hộp', N'Sữa & Chế phẩm sữa', 37000, 30, 10, N'Sữa tươi sạch TH true MILK 100% sữa bò tươi nguyên chất từ trang trại bò sữa hiện đại.', N'images/products/th-truemilk-180ml.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'ML-BOX')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'ML-BOX', N'Thức uống lúa mạch Nestlé Milo Activ-Go hũ 400g', N'Hũ', N'Sữa & Chế phẩm sữa', 78000, 25, 8, N'Bột sữa lúa mạch Milo Activ-Go giàu mầm lúa mạch, vitamin và khoáng chất cho trẻ năng động.', N'images/products/milo-180ml.jpg', SYSDATETIME());

-- 3. MÌ, MIẾN, PHỞ & THỰC PHẨM ĂN LIỀN
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OMA-SUON')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'OMA-SUON', N'Mì khoai tây Omachi sườn hầm ngũ quả gói 80g', N'Gói', N'Thực phẩm & Gia vị', 9500, 90, 30, N'Mì sợi khoai tây dai mướt kết hợp nước dùng sườn hầm ngũ quả thanh ngọt không lo nóng trong.', N'images/products/omachi.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OMA-BO')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'OMA-BO', N'Mì khoai tây Omachi xốt bò hầm gói 80g', N'Gói', N'Thực phẩm & Gia vị', 9500, 85, 30, N'Mì Omachi xốt bò hầm đậm đà hạt tiêu thơm phức nước xốt sánh đậm sánh quyện.', N'images/products/omachi.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HH-STH')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HH-STH', N'Mì Hảo Hảo Sa tế hành Acecook gói 75g', N'Gói', N'Thực phẩm & Gia vị', 4500, 110, 40, N'Mì ăn liền Hảo Hảo hương vị Sa tế hành nồng ấm kích thích vị giác thơm nức mũi.', N'images/products/haohao.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PHO-CD')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'PHO-CD', N'Phở bò Cung Đình gói 68g gói nước cốt xương hầm', N'Gói', N'Thực phẩm & Gia vị', 8500, 40, 15, N'Phở bò Cung Đình sợi gạo dẻo dai hòa quyện nước cốt xương hầm thảo mộc truyền thống.', N'images/products/omachi.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'MI-CUP-HH')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'MI-CUP-HH', N'Mì ly Hảo Hảo Handy tôm chua cay 67g kèm nĩa', N'Ly', N'Thực phẩm & Gia vị', 9000, 35, 12, N'Mì ly Handy Hảo Hảo tiện lợi ăn liền mọi lúc mọi nơi có sẵn thịt tôm sấy và rau thơm.', N'images/products/haohao.jpg', SYSDATETIME());

-- 4. GẠO, GIA VỊ, DẦU ĂN VÀ THỰC PHẨM KHÔ
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SIM-1L')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'SIM-1L', N'Dầu đậu nành nguyên chất Simply chai 1L cho trái tim khỏe', N'Chai', N'Thực phẩm & Gia vị', 62000, 40, 15, N'Dầu đậu nành nguyên chất Simply giàu Omega 3-6-9 tốt cho tim mạch chiên xào ngon miệng.', N'images/products/simply-1l.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'NEP-1L')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'NEP-1L', N'Dầu thực vật cao cấp Neptune Gold chai 1L', N'Chai', N'Thực phẩm & Gia vị', 64000, 35, 12, N'Dầu ăn Neptune Gold kết hợp 3 loại dầu tự nhiên đậu nành, gạo lứt và hướng dương hảo hạng.', N'images/products/simply.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CS-OT')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CS-OT', N'Nước tương Chinsu tỏi ớt chai 330ml cay thơm nồng', N'Chai', N'Thực phẩm & Gia vị', 18000, 50, 15, N'Nước tương đậu nành Chinsu ngâm tỏi ớt tươi cay nồng sánh quyện bùng nổ hương vị chấm.', N'images/products/chinsu.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'NN-750')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'NN-750', N'Nước mắm Nam Ngư Đệ Nhị chai 900ml', N'Chai', N'Thực phẩm & Gia vị', 33000, 55, 20, N'Nước mắm Nam Ngư Đệ Nhị thơm ngon tròn vị cá cơm cho bữa cơm gia đình thêm đậm đà.', N'images/products/namngu.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'KNR-400')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'KNR-400', N'Hạt nêm Knorr thịt thăn xương ống tủy gói 400g', N'Gói', N'Thực phẩm & Gia vị', 37000, 45, 15, N'Hạt nêm Knorr từ nguồn thịt thăn và xương ống hầm ngọt nước đậm vị chuẩn đầu bếp.', N'images/products/knorr.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CS-TUONG')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CS-TUONG', N'Tương ớt Chinsu cay nồng chai 250g', N'Chai', N'Thực phẩm & Gia vị', 14500, 70, 25, N'Tương ớt Chinsu đỏ mịn sóng sánh vị cay tự nhiên ăn kèm phở, bún, hải sản hoặc đồ chiên.', N'images/products/chinsu.jpg', SYSDATETIME());

-- 5. BÁNH KẸO VÀ ĐỒ ĂN VẶT
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CP-12')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CP-12', N'Bánh Chocopie Orion sôcôla marshmallow hộp 12 cái 360g', N'Hộp', N'Bánh kẹo & Đồ ăn vặt', 58000, 45, 15, N'Bánh Chocopie Orion lớp vỏ sôcôla ngọt dịu nhân dẻo marshmallow biểu tượng tình cảm gia đình.', N'images/products/chocopie-orion.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CS-MARIE')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CS-MARIE', N'Bánh quy sữa Cosy Marie Kinh Đô gói 336g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 34000, 40, 12, N'Bánh quy giòn xốp Cosy Marie thoảng hương bơ sữa ngọt dịu thích hợp ăn kèm tách trà nóng.', N'images/products/cosy-marie.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'LAY-NAT')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'LAY-NAT', N'Snack khoai tây chiên Lay''s vị tự nhiên Classic gói 56g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 13500, 65, 20, N'Khoai tây lát mỏng giòn rụm Lay''s chiên vàng ươm vị muối tự nhiên thơm ngậy ăn là mê.', N'images/products/lays.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OST-KC')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'OST-KC', N'Bánh lát khoai tây O''Star vị kim chi Hàn Quốc gói 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 13500, 55, 20, N'Khoai tây lát O''Star giòn xốp thấm đượm gia vị kim chi chua cay phong cách Hàn Quốc.', N'images/products/ostar-kimchi.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SW-STEAK')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'SW-STEAK', N'Snack khoai tây Swing vị bít tết bò New York gói 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 14000, 50, 18, N'Khoai tây lượn sóng Swing dày giòn sốt bò bít tết tiêu đen đậm đà cá tính.', N'images/products/swing-steak.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CP-PANDA')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CP-PANDA', N'Kẹo mút Chupa Chups hương dâu và cola gói 10 cây', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 16000, 40, 15, N'Kẹo mút Chupa Chups tròn xoe vị trái cây ngọt ngào tuổi thơ gắn kết tình bạn.', N'images/products/chupachups-panda.jpg', SYSDATETIME());

-- 6. CÀ PHÊ VÀ TRÀ
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'G7-3IN1')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'G7-3IN1', N'Cà phê hòa tan G7 3in1 Trung Nguyên hộp 18 gói x 16g', N'Hộp', N'Cà phê & Trà', 55000, 50, 15, N'Cà phê năng lượng G7 Trung Nguyên công nghệ nano giữ trọn hương vị cà phê rang xay đậm đặc.', N'images/products/c2.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'NES-3IN1')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'NES-3IN1', N'Cà phê hòa tan Nescafé 3in1 Đậm Đà Hài Hòa hộp 20 gói', N'Hộp', N'Cà phê & Trà', 58000, 42, 15, N'Cà phê Nescafé hương vị cà phê sữa sánh mịn bừng tỉnh ngày mới tỉnh táo làm việc.', N'images/products/c2.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TN-CHUS')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'TN-CHUS', N'Cà phê phin Trung Nguyên Chữ S gói 500g nguyên chất', N'Gói', N'Cà phê & Trà', 62000, 30, 10, N'Cà phê phin truyền thống Trung Nguyên Chữ S màu nâu cánh gián mùi thơm bền lâu.', N'images/products/c2.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'LIP-YEL')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'LIP-YEL', N'Trà túi lọc Lipton Yellow Label nhãn vàng hộp 25 túi', N'Hộp', N'Cà phê & Trà', 39000, 35, 10, N'Trà đen Lipton nhãn vàng tuyển chọn từ búp trà tươi giữ trọn chất chống oxy hóa tự nhiên.', N'images/products/khongdo-455ml.jpg', SYSDATETIME());

-- 7. CHĂM SÓC CÁ NHÂN
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CLR-630')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CLR-630', N'Dầu gội sạch gàu Clear Men bạc hà mát lạnh chai 630g', N'Chai', N'Hóa mỹ phẩm & Đồ gia dụng', 165000, 25, 8, N'Dầu gội Clear Men tinh chất bạc hà đánh bay gàu ngứa đem lại cảm giác sảng khoái suốt 24h.', N'images/products/romano-classic.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SUN-650')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'SUN-650', N'Dầu gội Sunsilk óng mượt rạng ngời chiết xuất bồ kết 650g', N'Chai', N'Hóa mỹ phẩm & Đồ gia dụng', 138000, 28, 8, N'Dầu gội Sunsilk bồ kết nuôi dưỡng mái tóc suôn mềm óng ả bồng bềnh dưới ánh nắng.', N'images/products/romano.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'DOVE-640')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'DOVE-640', N'Dầu gội Dove phục hồi hư tổn chuyên sâu chai 640g', N'Chai', N'Hóa mỹ phẩm & Đồ gia dụng', 162000, 20, 6, N'Dầu gội Dove công nghệ Bio-Nourish tái cấu trúc và nuôi dưỡng tóc khô xơ từ sâu bên trong.', N'images/products/romano.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PS-200')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'PS-200', N'Kem đánh răng P/S bảo vệ 123 chăm sóc toàn diện tuýp 200g', N'Hộp', N'Hóa mỹ phẩm & Đồ gia dụng', 32000, 60, 20, N'Kem đánh răng P/S canxi và flour ngừa sâu răng vượt trội cho hơi thở thơm mát tự tin.', N'images/products/colgate.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CG-TOT')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CG-TOT', N'Kem đánh răng Colgate Total than hoạt tính tuýp 150g', N'Hộp', N'Hóa mỹ phẩm & Đồ gia dụng', 48000, 40, 15, N'Kem đánh răng Colgate Total kháng khuẩn bảo vệ nướu và làm trắng răng tự nhiên.', N'images/products/colgate.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'ROM-650')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'ROM-650', N'Dầu gội nước hoa Romano Classic quyến rũ lịch lãm chai 650g', N'Chai', N'Hóa mỹ phẩm & Đồ gia dụng', 175000, 22, 8, N'Dầu gội hương nước hoa Romano lưu hương nam tính đẳng cấp dành cho phái mạnh.', N'images/products/romano-classic.jpg', SYSDATETIME());

-- 8. HÓA PHẨM GIA DỤNG
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OMO-36')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'OMO-36', N'Nước giặt OMO Matic cửa trên túi 3.6kg xoáy bay vết bẩn', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 185000, 28, 10, N'Nước giặt OMO Matic công thức bọt thông minh bảo vệ máy giặt và tẩy sạch mọi vết ố cứng đầu.', N'images/products/omo.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'ARI-32')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'ARI-32', N'Nước giặt Ariel Downy khử mùi ẩm mốc túi 3.2kg', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 182000, 24, 8, N'Nước giặt Ariel thế hệ mới hương Downy ngát hương khử sạch vi khuẩn gây mùi ẩm mốc mùa mưa.', N'images/products/omo.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'COMF-18')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'COMF-18', N'Nước xả vải Comfort hương ban mai tươi mát túi 1.8L', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 112000, 35, 12, N'Nước xả làm mềm vải Comfort sợi vải mềm mại thơm ngát suốt ngày dài năng động.', N'images/products/comfort-18l.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SL-750')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'SL-750', N'Nước rửa chén Sunlight tinh dầu chanh tươi chai 750g', N'Chai', N'Hóa mỹ phẩm & Đồ gia dụng', 27000, 55, 20, N'Nước rửa chén Sunlight chiết xuất chanh đánh tan dầu mỡ sạch bóng kin kít dịu nhẹ da tay.', N'images/products/sunlight-chanh.jpg', SYSDATETIME());

-- 9. GIẤY VÀ VẬT DỤNG GIA ĐÌNH
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PULP-10')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'PULP-10', N'Giấy vệ sinh lụa Pulppy 3 lớp mềm dai lốc 10 cuộn', N'Lốc', N'Hóa mỹ phẩm & Đồ gia dụng', 92000, 35, 10, N'Giấy cuộn vệ sinh Pulppy 100% bột giấy nguyên chất 3 lớp siêu thấm hút không hóa chất độc hại.', N'images/products/pulppy-10cuon.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PULP-BOX')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'PULP-BOX', N'Khăn giấy lụa rút Pulppy hộp 180 tờ 2 lớp', N'Hộp', N'Hóa mỹ phẩm & Đồ gia dụng', 26000, 45, 15, N'Khăn giấy ăn hộp Pulppy giấy mềm mại dai mịn an toàn cho da nhạy cảm gia đình.', N'images/products/pulppy.jpg', SYSDATETIME());

-- 10. THỰC PHẨM ĐÓNG HỘP VÀ ĐÓNG GÓI
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HEO-VS')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HEO-VS', N'Thịt heo hầm Vissan đóng hộp tiện lợi 150g', N'Hộp', N'Thực phẩm & Gia vị', 32000, 35, 10, N'Thịt heo hầm Vissan thơm ngon đậm đà gia vị ăn liền với cơm trắng hoặc bánh mì giòn.', N'images/products/bacogai-155g.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PATE-HL')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'PATE-HL', N'Pate gan heo Hạ Long Canfoco hộp 170g truyền thống', N'Hộp', N'Thực phẩm & Gia vị', 24500, 40, 12, N'Pate gan Hạ Long béo ngậy mùi thơm đặc trưng điểm tâm ăn sáng tiện lợi cho cả gia đình.', N'images/products/bacogai-155g.jpg', SYSDATETIME());

PRINT N'Hoàn thành nạp dữ liệu! Tổng sản phẩm hiện tại:';
SELECT COUNT(*) AS TongSoSanPham FROM dbo.Product;
GO
