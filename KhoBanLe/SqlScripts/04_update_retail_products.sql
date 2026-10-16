-- =========================================================================
-- SCRIPT CẬP NHẬT & BỔ SUNG HÀNG HÓA BÁN LẺ THỰC TẾ CHO KHOBANLE
-- Đồng bộ chuẩn 100% CSDL SQL Server & Ứng dụng Bán lẻ POS
-- =========================================================================

USE [KhoBanLe];
GO

-- 1. Chuẩn hóa & Cập nhật các sản phẩm từ 1 đến 8 (Giữ nguyên Id để không gãy liên kết FK)
UPDATE dbo.Product 
SET Sku = N'TL-01', Name = N'Bút bi Thiên Long TL-027 xanh', Unit = N'Cái', Category = N'Văn phòng phẩm',
    Price = 5000, MinStockLevel = 20, 
    Description = N'Bút bi Thiên Long mực xanh cao cấp ngòi 0.5mm êm trơn. (Mặt hàng Demo Luật 1: Tồn kho chỉ có 5 cái)',
    ImageUrl = N'https://images.unsplash.com/photo-1583485088034-697b5bc54ccd?w=400'
WHERE Id = 1;

UPDATE dbo.Product 
SET Sku = N'HH-01', Name = N'Mì Hảo Hảo Tôm chua cay (thùng 30 gói)', Unit = N'Thùng', Category = N'Thực phẩm & Gia vị',
    Price = 115000, MinStockLevel = 25, 
    Description = N'Mì ăn liền Acecook Hảo Hảo vị Tôm chua cay truyền thống thùng 30 gói x 75g.',
    ImageUrl = N'https://images.unsplash.com/photo-1612927601601-6638404737ce?w=400'
WHERE Id = 2;

UPDATE dbo.Product 
SET Sku = N'VM-01', Name = N'Sữa tươi tiệt trùng Vinamilk 100% 1L', Unit = N'Hộp', Category = N'Sữa & Chế phẩm sữa',
    Price = 34000, MinStockLevel = 15, 
    Description = N'Sữa tươi tiệt trùng Vinamilk có đường 1 lít giàu canxi và vitamin D3.',
    ImageUrl = N'https://images.unsplash.com/photo-1550583724-b2692b85b150?w=400'
WHERE Id = 3;

UPDATE dbo.Product 
SET Sku = N'CC-01', Name = N'Nước ngọt Coca-Cola lon 320ml (thùng 24 lon)', Unit = N'Thùng', Category = N'Nước giải khát & Bia',
    Price = 210000, MinStockLevel = 20, 
    Description = N'Nước giải khát có ga Coca-Cola sảng khoái nguyên thùng 24 lon x 320ml.',
    ImageUrl = N'https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=400'
WHERE Id = 4;

UPDATE dbo.Product 
SET Sku = N'ST25-05', Name = N'Gạo thơm Sóc Trăng ST25 thượng hạng 5kg', Unit = N'Túi', Category = N'Thực phẩm & Gia vị',
    Price = 185000, MinStockLevel = 10, 
    Description = N'Gạo ST25 chuẩn thương hiệu đặc sản Sóc Trăng, hạt dài, dẻo thơm hảo hạng.',
    ImageUrl = N'https://images.unsplash.com/photo-1586201375761-83865001e31c?w=400'
WHERE Id = 5;

UPDATE dbo.Product 
SET Sku = N'SP-01', Name = N'Dầu ăn đậu nành Simply nguyên chất 1L', Unit = N'Chai', Category = N'Thực phẩm & Gia vị',
    Price = 58000, MinStockLevel = 10, 
    Description = N'Dầu đậu nành nguyên chất 100% tốt cho tim mạch chai 1 lít.',
    ImageUrl = N'https://images.unsplash.com/photo-1474979266404-7eaacbcd87c5?w=400'
WHERE Id = 6;

UPDATE dbo.Product 
SET Sku = N'SL-01', Name = N'Nước rửa chén Sunlight Chanh túi 750g', Unit = N'Túi', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 32000, MinStockLevel = 15, 
    Description = N'Nước rửa chén Sunlight chiết xuất chanh tươi sạch dầu mỡ (Cảnh báo sắp hết hàng: tồn 6)',
    ImageUrl = N'https://images.unsplash.com/photo-1585421514284-efb74c2b69ba?w=400'
WHERE Id = 7;

UPDATE dbo.Product 
SET Sku = N'PP-01', Name = N'Khăn giấy lụa cao cấp Pulppy 10 cuộn', Unit = N'Lốc', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 85000, MinStockLevel = 10, 
    Description = N'Khăn giấy lụa vệ sinh Pulppy mềm mại an toàn (Cảnh báo hết hàng: Tồn 0)',
    ImageUrl = N'https://images.unsplash.com/photo-1584556812952-905ffd0c611a?w=400'
WHERE Id = 8;

-- 2. Cập nhật các mã từ 9 đến 20 thành mặt hàng bán lẻ thực tế (Tránh vỡ khóa ngoại Id 11)
UPDATE dbo.Product 
SET Sku = N'CC-LON', Name = N'Nước ngọt Coca-Cola vị nguyên bản lon 320ml', Unit = N'Lon', Category = N'Nước giải khát & Bia',
    Price = 11000, CurrentStock = 150, MinStockLevel = 24, 
    Description = N'Nước giải khát có ga Coca-Cola sảng khoái lon lẻ 320ml bán chạy nhất tại quầy.',
    ImageUrl = N'https://images.unsplash.com/photo-1554866585-cd94860890b7?w=400'
WHERE Id = 9;

UPDATE dbo.Product 
SET Sku = N'RB-01', Name = N'Nước tăng lực Red Bull Thái lon 250ml', Unit = N'Lon', Category = N'Nước giải khát & Bia',
    Price = 15000, CurrentStock = 120, MinStockLevel = 24, 
    Description = N'Nước tăng lực bò húc Red Bull nắp vàng Thái Lan chính hãng giúp tỉnh táo tập trung.',
    ImageUrl = N'https://images.unsplash.com/photo-1622543925917-763c34d1a86e?w=400'
WHERE Id = 10;

UPDATE dbo.Product 
SET Sku = N'RM-650', Name = N'Dầu gội nam Romano Classic sạch gàu 650g', Unit = N'Chai', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 175000, CurrentStock = 45, MinStockLevel = 10, 
    Description = N'Dầu gội nước hoa cao cấp Romano hương Classic lịch lãm, sạch gàu nuôi dưỡng tóc.',
    ImageUrl = N'https://images.unsplash.com/photo-1535585209827-a15fcdbc4c2d?w=400'
WHERE Id = 11;

UPDATE dbo.Product 
SET Sku = N'LAY-01', Name = N'Snack khoai tây Lay''s vị tự nhiên Classic 95g', Unit = N'Gói', Category = N'Bánh kẹo & Đồ ăn vặt',
    Price = 22000, CurrentStock = 85, MinStockLevel = 15, 
    Description = N'Bim bim khoai tây lát mỏng chiên vàng giòn rụm vị muối tự nhiên thơm ngon.',
    ImageUrl = N'https://images.unsplash.com/photo-1566478989037-eec170784d0b?w=400'
WHERE Id = 12;

UPDATE dbo.Product 
SET Sku = N'CP-12', Name = N'Bánh ChocoPie Orion hộp 12 cái (396g)', Unit = N'Hộp', Category = N'Bánh kẹo & Đồ ăn vặt',
    Price = 56000, CurrentStock = 60, MinStockLevel = 10, 
    Description = N'Bánh sôcôla ChocoPie kẹp kem dẻo marshmallow thơm lừng ngọt ngào.',
    ImageUrl = N'https://images.unsplash.com/photo-1606313564200-e75d5e30476c?w=400'
WHERE Id = 13;

UPDATE dbo.Product 
SET Sku = N'LV-500', Name = N'Nước khoáng thiên nhiên LaVie chai 500ml', Unit = N'Chai', Category = N'Nước giải khát & Bia',
    Price = 6000, CurrentStock = 200, MinStockLevel = 48, 
    Description = N'Nước khoáng LaVie đóng chai tự nhiên giải khát thanh lọc cơ thể hàng ngày.',
    ImageUrl = N'https://images.unsplash.com/photo-1548839140-29a749e1bc4e?w=400'
WHERE Id = 14;

UPDATE dbo.Product 
SET Sku = N'HN-330', Name = N'Bia Heineken Silver lon 330ml', Unit = N'Lon', Category = N'Nước giải khát & Bia',
    Price = 21000, CurrentStock = 95, MinStockLevel = 24, 
    Description = N'Bia Heineken Silver nhẹ êm dễ uống, hương vị tươi mát sảng khoái.',
    ImageUrl = N'https://images.unsplash.com/photo-1608270116805-728b525db313?w=400'
WHERE Id = 15;

UPDATE dbo.Product 
SET Sku = N'VM-LOC', Name = N'Sữa tươi Vinamilk có đường 180ml (lốc 4 hộp)', Unit = N'Lốc', Category = N'Sữa & Chế phẩm sữa',
    Price = 34000, CurrentStock = 80, MinStockLevel = 20, 
    Description = N'Lốc 4 hộp sữa tươi tiệt trùng Vinamilk có đường giàu dinh dưỡng cho cả gia đình.',
    ImageUrl = N'https://images.unsplash.com/photo-1563636619-e9143da7973b?w=400'
WHERE Id = 16;

UPDATE dbo.Product 
SET Sku = N'SC-VM', Name = N'Sữa chua ăn Vinamilk có đường (lốc 4 hộp x 100g)', Unit = N'Lốc', Category = N'Sữa & Chế phẩm sữa',
    Price = 28000, CurrentStock = 70, MinStockLevel = 15, 
    Description = N'Sữa chua lên men tự nhiên bổ sung men vi sinh và vitamin tốt cho tiêu hóa.',
    ImageUrl = N'https://images.unsplash.com/photo-1488477181946-6428a0291777?w=400'
WHERE Id = 17;

UPDATE dbo.Product 
SET Sku = N'OT-380', Name = N'Sữa đặc có đường Ông Thọ đỏ lon 380g', Unit = N'Lon', Category = N'Sữa & Chế phẩm sữa',
    Price = 26000, CurrentStock = 90, MinStockLevel = 20, 
    Description = N'Sữa đặc Ông Thọ sánh đặc ngọt béo dùng pha cà phê, làm bánh cực thơm ngon.',
    ImageUrl = N'https://images.unsplash.com/photo-1528751014936-863e6e7a319c?w=400'
WHERE Id = 18;

UPDATE dbo.Product 
SET Sku = N'HH-GOI', Name = N'Mì Hảo Hảo tôm chua cay gói 75g', Unit = N'Gói', Category = N'Thực phẩm & Gia vị',
    Price = 4500, CurrentStock = 300, MinStockLevel = 50, 
    Description = N'Mì gói Hảo Hảo bán lẻ từng gói, sợi mì vàng dai giòn hòa quyện nước súp chua cay.',
    ImageUrl = N'https://images.unsplash.com/photo-1569718212165-3a8278d5f624?w=400'
WHERE Id = 19;

UPDATE dbo.Product 
SET Sku = N'NN-900', Name = N'Nước mắm Nam Ngư Đệ Nhị chai 900ml', Unit = N'Chai', Category = N'Thực phẩm & Gia vị',
    Price = 32000, CurrentStock = 65, MinStockLevel = 15, 
    Description = N'Nước mắm Nam Ngư Đệ Nhị vị ngon đậm đà, chấm hay nấu đều tròn vị.',
    ImageUrl = N'https://images.unsplash.com/photo-1589301760014-d929f3979dbc?w=400'
WHERE Id = 20;

-- 3. Thêm các mặt hàng bán lẻ bổ sung mới nếu chưa có
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'KN-400')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'KN-400', N'Hạt nêm Knorr thịt thăn xương ống 400g', N'Gói', N'Thực phẩm & Gia vị', 39000, 55, 15, 
            N'Hạt nêm Knorr chiết xuất thịt thăn và tủy xương cho món canh ngọt thanh.', 
            N'https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CS-250')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CS-250', N'Tương ớt Chinsu chai 250g', N'Chai', N'Thực phẩm & Gia vị', 14000, 70, 20, 
            N'Tương ớt Chinsu cay nồng thơm lừng vị ớt tươi, ăn kèm mọi món ăn.', 
            N'https://images.unsplash.com/photo-1588615419957-c6b7582b1fae?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CG-180')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CG-180', N'Kem đánh răng Colgate Total than hoạt tính 180g', N'Hộp', N'Hóa mỹ phẩm & Đồ gia dụng', 45000, 50, 15, 
            N'Kem đánh răng Colgate ngừa sâu răng, hơi thở thơm mát suốt 12 giờ.', 
            N'https://images.unsplash.com/photo-1559591937-e10b1ddc3b28?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OM-2KG')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OM-2KG', N'Bột giặt Omo Matic cửa trên túi 2kg', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 135000, 30, 10, 
            N'Bột giặt Omo Matic xoáy bay vết bẩn cứng đầu, bảo vệ màu sợi vải.', 
            N'https://images.unsplash.com/photo-1585421514738-01798e348b17?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CF-18')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CF-18', N'Nước xả vải Comfort một lần xả túi 1.8L', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 118000, 40, 10, 
            N'Nước xả vải Comfort hương ban mai thơm ngát bền lâu, mềm mại quần áo.', 
            N'https://images.unsplash.com/photo-1584813470613-5b1c1cad3d69?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CC-PD')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CC-PD', N'Kẹo dẻo Chupa Chups Panda vị trái cây 90g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 16000, 100, 20, 
            N'Kẹo dẻo hình gấu Panda hương trái cây nhiệt đới thơm ngon vui nhộn.', 
            N'https://images.unsplash.com/photo-1582058091505-f87a2e55a40f?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CS-288')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CS-288', N'Bánh quy Cosy Marie Kinh Đô 288g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 38000, 45, 10, 
            N'Bánh quy sữa giòn xốp Cosy Marie thơm bơ thích hợp ăn sáng và tiệc trà.', 
            N'https://images.unsplash.com/photo-1509440159596-0249088772ff?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CP-A5')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CP-A5', N'Sổ tay Campus A5 kẻ ngang 200 trang', N'Quyển', N'Văn phòng phẩm', 25000, 60, 15, 
            N'Sổ tay lò xo bìa cứng Campus giấy chống lóa viết êm trơn không lem mực.', 
            N'https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'C2-01')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'C2-01', N'Trà xanh C2 hương chanh chai 455ml', N'Chai', N'Nước giải khát & Bia', 10000, 85, 24, 
            N'Trà xanh đóng chai C2 ủ từ lá trà Thái Nguyên nguyên chất vị chanh thanh mát.', 
            N'https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=400');
END
GO
