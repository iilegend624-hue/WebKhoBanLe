-- =========================================================================
-- SCRIPT CẬP NHẬT 100% TIẾNG VIỆT CHUẨN VÀ ẢNH PACKSHOT NỘI BỘ
-- Khắc phục triệt để lỗi Font chữ (Mojibake) và lỗi ảnh xộc xệch/placeholder
-- =========================================================================

USE [KhoBanLe];
GO

-- 1. TL-01: Bút bi Thiên Long
UPDATE dbo.Product 
SET Sku = N'TL-01', Name = N'Bút bi Thiên Long TL-027 xanh', Unit = N'Cái', Category = N'Văn phòng phẩm',
    Price = 5000, MinStockLevel = 20, 
    Description = N'Bút bi Thiên Long mực xanh cao cấp ngòi 0.5mm êm trơn. (Mặt hàng Demo Luật 1: Tồn kho chỉ có 5 cái)',
    ImageUrl = N'images/products/thienlong.jpg'
WHERE Id = 1;

-- 2. HH-01: Mì Hảo Hảo Thùng
UPDATE dbo.Product 
SET Sku = N'HH-01', Name = N'Mì Hảo Hảo Tôm chua cay (thùng 30 gói)', Unit = N'Thùng', Category = N'Mì gói & Thực phẩm',
    Price = 115000, MinStockLevel = 25, 
    Description = N'Mì ăn liền Acecook Hảo Hảo vị Tôm chua cay truyền thống thùng 30 gói x 75g.',
    ImageUrl = N'images/products/haohao.jpg'
WHERE Id = 2;

-- 3. VM-01: Sữa tươi Vinamilk 1L
UPDATE dbo.Product 
SET Sku = N'VM-01', Name = N'Sữa tươi tiệt trùng Vinamilk 100% 1L', Unit = N'Hộp', Category = N'Sữa & Chế phẩm sữa',
    Price = 34000, MinStockLevel = 15, 
    Description = N'Sữa tươi tiệt trùng Vinamilk có đường 1 lít giàu canxi và vitamin D3.',
    ImageUrl = N'images/products/vinamilk.jpg'
WHERE Id = 3;

-- 4. CC-01: Nước ngọt Coca-Cola Thùng
UPDATE dbo.Product 
SET Sku = N'CC-01', Name = N'Nước ngọt Coca-Cola lon 320ml (thùng 24 lon)', Unit = N'Thùng', Category = N'Nước giải khát & Bia',
    Price = 210000, MinStockLevel = 20, 
    Description = N'Nước giải khát có ga Coca-Cola sảng khoái nguyên thùng 24 lon x 320ml.',
    ImageUrl = N'images/products/coca-cola.png'
WHERE Id = 4;

-- 5. ST25-05: Gạo thơm Sóc Trăng ST25
UPDATE dbo.Product 
SET Sku = N'ST25-05', Name = N'Gạo thơm Sóc Trăng ST25 thượng hạng 5kg', Unit = N'Túi', Category = N'Thực phẩm & Gia vị',
    Price = 185000, MinStockLevel = 10, 
    Description = N'Gạo ST25 chuẩn thương hiệu đặc sản Sóc Trăng, hạt dài, dẻo thơm hảo hạng.',
    ImageUrl = N'images/products/st25.jpg'
WHERE Id = 5;

-- 6. SP-01: Dầu ăn Simply
UPDATE dbo.Product 
SET Sku = N'SP-01', Name = N'Dầu ăn đậu nành Simply nguyên chất 1L', Unit = N'Chai', Category = N'Thực phẩm & Gia vị',
    Price = 58000, MinStockLevel = 10, 
    Description = N'Dầu đậu nành nguyên chất 100% tốt cho tim mạch chai 1 lít.',
    ImageUrl = N'images/products/simply.jpg'
WHERE Id = 6;

-- 7. SL-01: Nước rửa chén Sunlight
UPDATE dbo.Product 
SET Sku = N'SL-01', Name = N'Nước rửa chén Sunlight Chanh túi 750g', Unit = N'Túi', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 32000, MinStockLevel = 15, 
    Description = N'Nước rửa chén Sunlight chiết xuất chanh tươi sạch dầu mỡ (Cảnh báo sắp hết hàng: tồn 6)',
    ImageUrl = N'images/products/sunlight.jpg'
WHERE Id = 7;

-- 8. PP-01: Khăn giấy Pulppy
UPDATE dbo.Product 
SET Sku = N'PP-01', Name = N'Khăn giấy lụa cao cấp Pulppy 10 cuộn', Unit = N'Lốc', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 85000, MinStockLevel = 10, 
    Description = N'Khăn giấy vệ sinh cuộn Pulppy 3 lớp mềm mại và dai vượt trội.',
    ImageUrl = N'images/products/pulppy.jpg'
WHERE Id = 8;

-- 9. CC-LON: Nước ngọt Coca-Cola lon lẻ
UPDATE dbo.Product 
SET Sku = N'CC-LON', Name = N'Nước ngọt Coca-Cola vị nguyên bản lon 320ml', Unit = N'Lon', Category = N'Nước giải khát & Bia',
    Price = 10000, MinStockLevel = 24, 
    Description = N'Nước giải khát có ga Coca-Cola vị nguyên bản lon 320ml chính hãng sảng khoái.',
    ImageUrl = N'images/products/coca-cola.png'
WHERE Sku = N'CC-LON' OR Id = 9;

-- 10. RB-01: Nước tăng lực Red Bull
UPDATE dbo.Product 
SET Sku = N'RB-01', Name = N'Nước tăng lực Red Bull Thái lon 250ml', Unit = N'Lon', Category = N'Nước giải khát & Bia',
    Price = 13000, MinStockLevel = 24, 
    Description = N'Nước tăng lực Red Bull nắp vàng nhập khẩu Thái Lan lon 250ml tỉnh táo tức thì.',
    ImageUrl = N'images/products/redbull.jpg'
WHERE Sku = N'RB-01' OR Id = 10;

-- 11. RM-650: Dầu gội Romano Classic
UPDATE dbo.Product 
SET Sku = N'RM-650', Name = N'Dầu gội nam Romano Classic sạch gàu 650g', Unit = N'Chai', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 175000, MinStockLevel = 10, 
    Description = N'Dầu gội nước hoa cao cấp Romano Classic hương gỗ nam tính lịch lãm.',
    ImageUrl = N'images/products/romano.jpg'
WHERE Sku = N'RM-650' OR Id = 11;

-- 12. LAY-01: Snack khoai tây Lay''s
UPDATE dbo.Product 
SET Sku = N'LAY-01', Name = N'Snack khoai tây Lay''s vị tự nhiên Classic 95g', Unit = N'Gói', Category = N'Bánh kẹo & Đồ ăn vặt',
    Price = 22000, MinStockLevel = 20, 
    Description = N'Snack khoai tây Lay''s vị muối tự nhiên Classic giòn tan vàng ruộm thơm ngon.',
    ImageUrl = N'images/products/lays.jpg'
WHERE Sku = N'LAY-01' OR Id = 12;

-- 13. CP-12: Bánh ChocoPie Orion
UPDATE dbo.Product 
SET Sku = N'CP-12', Name = N'Bánh ChocoPie Orion hộp 12 cái (396g)', Unit = N'Hộp', Category = N'Bánh kẹo & Đồ ăn vặt',
    Price = 56000, MinStockLevel = 15, 
    Description = N'Bánh ChocoPie Orion phủ sô-cô-la nhân kẹo dẻo marshmallow mềm mịn thơm ngon.',
    ImageUrl = N'images/products/chocopie.jpg'
WHERE Sku = N'CP-12' OR Id = 13;

-- 14. LV-500: Nước khoáng LaVie
UPDATE dbo.Product 
SET Sku = N'LV-500', Name = N'Nước khoáng thiên nhiên LaVie chai 500ml', Unit = N'Chai', Category = N'Nước giải khát & Bia',
    Price = 6000, MinStockLevel = 30, 
    Description = N'Nước khoáng thiên nhiên LaVie 500ml giàu khoáng chất tự nhiên thanh khiết.',
    ImageUrl = N'images/products/lavie.jpg'
WHERE Sku = N'LV-500' OR Id = 14;

-- 15. HN-330: Bia Heineken Silver
UPDATE dbo.Product 
SET Sku = N'HN-330', Name = N'Bia Heineken Silver lon 330ml', Unit = N'Lon', Category = N'Nước giải khát & Bia',
    Price = 21000, MinStockLevel = 24, 
    Description = N'Bia Heineken Silver lon bạc 330ml êm đằm dễ uống hương vị sảng khoái.',
    ImageUrl = N'images/products/heineken.jpg'
WHERE Sku = N'HN-330' OR Id = 15;

-- 16. VM-LOC: Sữa tươi Vinamilk lốc 4 hộp
UPDATE dbo.Product 
SET Sku = N'VM-LOC', Name = N'Sữa tươi Vinamilk có đường 180ml (lốc 4 hộp)', Unit = N'Lốc', Category = N'Sữa & Chế phẩm sữa',
    Price = 32000, MinStockLevel = 20, 
    Description = N'Lốc 4 hộp sữa tươi tiệt trùng Vinamilk có đường 180ml bổ dưỡng.',
    ImageUrl = N'images/products/vinamilk.jpg'
WHERE Sku = N'VM-LOC' OR Id = 16;

-- 17. SC-VM: Sữa chua Vinamilk
UPDATE dbo.Product 
SET Sku = N'SC-VM', Name = N'Sữa chua ăn Vinamilk có đường (lốc 4 hộp x 100g)', Unit = N'Lốc', Category = N'Sữa & Chế phẩm sữa',
    Price = 28000, MinStockLevel = 15, 
    Description = N'Sữa chua ăn Vinamilk lên men tự nhiên giàu lợi khuẩn tốt cho đường ruột.',
    ImageUrl = N'images/products/yogurt.jpg'
WHERE Sku = N'SC-VM' OR Id = 17;

-- 18. OT-380: Sữa đặc Ông Thọ
UPDATE dbo.Product 
SET Sku = N'OT-380', Name = N'Sữa đặc có đường Ông Thọ đỏ lon 380g', Unit = N'Lon', Category = N'Sữa & Chế phẩm sữa',
    Price = 26000, MinStockLevel = 20, 
    Description = N'Sữa đặc có đường Ông Thọ nhãn đỏ lon 380g sánh đặc ngọt béo.',
    ImageUrl = N'images/products/ongtho.jpg'
WHERE Sku = N'OT-380' OR Id = 18;

-- 19. HH-GOI: Mì Hảo Hảo gói
UPDATE dbo.Product 
SET Sku = N'HH-GOI', Name = N'Mì Hảo Hảo tôm chua cay gói 75g', Unit = N'Gói', Category = N'Mì gói & Thực phẩm',
    Price = 4500, MinStockLevel = 50, 
    Description = N'Mì gói Hảo Hảo bán lẻ từng gói, sợi mì vàng dai giòn hòa quyện nước súp chua cay.',
    ImageUrl = N'images/products/haohao.jpg'
WHERE Sku = N'HH-GOI' OR Id = 19;

-- 20. NN-900: Nước mắm Nam Ngư
UPDATE dbo.Product 
SET Sku = N'NN-900', Name = N'Nước mắm Nam Ngư Đệ Nhị chai 900ml', Unit = N'Chai', Category = N'Thực phẩm & Gia vị',
    Price = 32000, MinStockLevel = 15, 
    Description = N'Nước mắm Nam Ngư Đệ Nhị vị ngon đậm đà, chấm hay nấu đều tròn vị.',
    ImageUrl = N'images/products/namngu.jpg'
WHERE Sku = N'NN-900' OR Id = 20;

-- 21. KN-400: Hạt nêm Knorr
UPDATE dbo.Product 
SET Sku = N'KN-400', Name = N'Hạt nêm Knorr thịt thăn xương ống 400g', Unit = N'Gói', Category = N'Thực phẩm & Gia vị',
    Price = 39000, MinStockLevel = 15, 
    Description = N'Hạt nêm Knorr chiết xuất thịt thăn và tủy xương cho món canh ngọt thanh.',
    ImageUrl = N'images/products/knorr.jpg'
WHERE Sku = N'KN-400' OR Id = 21;

-- 22. CS-250: Tương ớt Chinsu
UPDATE dbo.Product 
SET Sku = N'CS-250', Name = N'Tương ớt Chinsu chai 250g', Unit = N'Chai', Category = N'Thực phẩm & Gia vị',
    Price = 14000, MinStockLevel = 20, 
    Description = N'Tương ớt Chinsu cay nồng thơm lừng vị ớt tươi, ăn kèm mọi món ăn.',
    ImageUrl = N'images/products/chinsu.jpg'
WHERE Sku = N'CS-250' OR Id = 22;

-- 23. CG-180: Kem đánh răng Colgate
UPDATE dbo.Product 
SET Sku = N'CG-180', Name = N'Kem đánh răng Colgate Total than hoạt tính 180g', Unit = N'Hộp', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 45000, MinStockLevel = 15, 
    Description = N'Kem đánh răng Colgate ngừa sâu răng, hơi thở thơm mát suốt 12 giờ.',
    ImageUrl = N'images/products/colgate.jpg'
WHERE Sku = N'CG-180' OR Id = 23;

-- 24. OM-2KG: Bột giặt Omo Matic
UPDATE dbo.Product 
SET Sku = N'OM-2KG', Name = N'Bột giặt Omo Matic cửa trên túi 2kg', Unit = N'Túi', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 135000, MinStockLevel = 10, 
    Description = N'Bột giặt Omo Matic xoáy bay vết bẩn cứng đầu, bảo vệ màu sợi vải.',
    ImageUrl = N'images/products/omo.jpg'
WHERE Sku = N'OM-2KG' OR Id = 24;

-- 25. CF-18: Nước xả vải Comfort
UPDATE dbo.Product 
SET Sku = N'CF-18', Name = N'Nước xả vải Comfort một lần xả túi 1.8L', Unit = N'Túi', Category = N'Hóa mỹ phẩm & Đồ gia dụng',
    Price = 118000, MinStockLevel = 10, 
    Description = N'Nước xả vải Comfort hương ban mai thơm ngát bền lâu, mềm mại quần áo.',
    ImageUrl = N'images/products/comfort.jpg'
WHERE Sku = N'CF-18' OR Id = 25;

-- 26. CC-PD: Kẹo dẻo Chupa Chups
UPDATE dbo.Product 
SET Sku = N'CC-PD', Name = N'Kẹo dẻo Chupa Chups Panda vị trái cây 90g', Unit = N'Gói', Category = N'Bánh kẹo & Đồ ăn vặt',
    Price = 16000, MinStockLevel = 20, 
    Description = N'Kẹo dẻo hình gấu Panda hương trái cây nhiệt đới thơm ngon vui nhộn.',
    ImageUrl = N'images/products/chupachups.jpg'
WHERE Sku = N'CC-PD' OR Id = 26;

-- 27. CS-288: Bánh quy Cosy Marie
UPDATE dbo.Product 
SET Sku = N'CS-288', Name = N'Bánh quy Cosy Marie Kinh Đô 288g', Unit = N'Gói', Category = N'Bánh kẹo & Đồ ăn vặt',
    Price = 38000, MinStockLevel = 10, 
    Description = N'Bánh quy sữa giòn xốp Cosy Marie thơm bơ thích hợp ăn sáng và tiệc trà.',
    ImageUrl = N'images/products/cosy.jpg'
WHERE Sku = N'CS-288' OR Id = 27;

-- 28. CP-A5: Sổ tay Campus A5
UPDATE dbo.Product 
SET Sku = N'CP-A5', Name = N'Sổ tay Campus A5 kẻ ngang 200 trang', Unit = N'Quyển', Category = N'Văn phòng phẩm',
    Price = 25000, MinStockLevel = 15, 
    Description = N'Sổ tay lò xo bìa cứng Campus giấy chống lóa viết êm trơn không lem mực.',
    ImageUrl = N'images/products/campus.jpg'
WHERE Sku = N'CP-A5' OR Id = 28;

-- 29. C2-01: Trà xanh C2
UPDATE dbo.Product 
SET Sku = N'C2-01', Name = N'Trà xanh C2 hương chanh chai 455ml', Unit = N'Chai', Category = N'Nước giải khát & Bia',
    Price = 10000, MinStockLevel = 24, 
    Description = N'Trà xanh đóng chai C2 ủ từ lá trà Thái Nguyên nguyên chất vị chanh thanh mát.',
    ImageUrl = N'images/products/c2.jpg'
WHERE Sku = N'C2-01' OR Id = 29;

GO
