-- =========================================================================
-- SCRIPT 06: THÊM CÁC MẶT HÀNG TẠP HÓA & SNACK KỆ HÀNG BÁN LẺ
-- Bổ sung các mặt hàng snack Orion, Swing, O'Star, Toonies, Sting, Omachi...
-- =========================================================================

USE [KhoBanLe];
GO

-- Cập nhật lại ImageUrl chuẩn cho sản phẩm hiện có nếu cần
UPDATE dbo.Product SET ImageUrl = N'images/products/heineken.jpg' WHERE Sku = N'HN-330';
UPDATE dbo.Product SET ImageUrl = N'images/products/c2.jpg' WHERE Sku = N'C2-01';
UPDATE dbo.Product SET ImageUrl = N'images/products/lavie.jpg' WHERE Sku = N'LV-500';
UPDATE dbo.Product SET ImageUrl = N'images/products/chocopie.jpg' WHERE Sku = N'CP-12';
UPDATE dbo.Product SET ImageUrl = N'images/products/sunlight.jpg' WHERE Sku = N'SL-01';
UPDATE dbo.Product SET ImageUrl = N'images/products/comfort.jpg' WHERE Sku = N'CF-18';
UPDATE dbo.Product SET ImageUrl = N'images/products/romano.jpg' WHERE Sku = N'RM-650';
UPDATE dbo.Product SET ImageUrl = N'images/products/ongtho.jpg' WHERE Sku = N'OT-380';
UPDATE dbo.Product SET ImageUrl = N'images/products/simply.jpg' WHERE Sku = N'SP-01';
UPDATE dbo.Product SET ImageUrl = N'images/products/pulppy.jpg' WHERE Sku = N'PP-01';
UPDATE dbo.Product SET ImageUrl = N'images/products/st25.jpg' WHERE Sku = N'ST25-05';
UPDATE dbo.Product SET ImageUrl = N'images/products/cosy.jpg' WHERE Sku = N'CS-288';
UPDATE dbo.Product SET ImageUrl = N'images/products/yogurt.jpg' WHERE Sku = N'SC-VM';
UPDATE dbo.Product SET ImageUrl = N'images/products/thienlong.jpg' WHERE Sku = N'TL-01';
UPDATE dbo.Product SET ImageUrl = N'images/products/campus.jpg' WHERE Sku = N'CP-A5';
UPDATE dbo.Product SET ImageUrl = N'images/products/chupachups.jpg' WHERE Sku = N'CC-PD';

-- 30. SW-NY: Snack Swing vị Bít tết New York
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SW-NY')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'SW-NY', N'Snack khoai tây Swing vị Bít tết New York 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 12500, 65, 15, 
            N'Snack khoai tây lát gợn sóng Swing vị Bít tết New York đậm đà giòn rụm.', N'images/products/swing-steak.jpg');
END

-- 31. SW-GA: Snack Swing vị Cánh gà nướng mật ong
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SW-GA')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'SW-GA', N'Snack khoai tây Swing vị Cánh gà nướng mật ong 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 12500, 50, 15, 
            N'Snack khoai tây Swing cánh gà nướng thơm lừng vị mật ong hấp dẫn.', N'images/products/swing-chicken.jpg');
END

-- 32. OST-TN: Snack O'Star vị Tự nhiên muối biển
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OST-TN')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OST-TN', N'Snack khoai tây O''Star vị tự nhiên muối biển 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 12000, 80, 20, 
            N'Snack khoai tây O''Star vị muối biển tự nhiên giòn tan thơm bùi đặc trưng.', N'images/products/ostar-salt.jpg');
END

-- 33. OST-KC: Snack O'Star vị Kim chi Hàn Quốc
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OST-KC')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OST-KC', N'Snack khoai tây O''Star vị Kim chi Hàn Quốc 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 12000, 75, 20, 
            N'Snack khoai tây lát O''Star vị Kim chi cay nhẹ chua ngọt đậm đà chuẩn vị Hàn.', N'images/products/ostar-kimchi.jpg');
END

-- 34. OST-TB: Snack O'Star vị Tảo biển
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OST-TB')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OST-TB', N'Snack khoai tây O''Star vị Tảo biển 63g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 12000, 60, 15, 
            N'Snack khoai tây O''Star rắc tảo biển thơm lừng giòn tan vui miệng.', N'images/products/ostar-seaweed.jpg');
END

-- 35. TOO-PM: Snack Toonies vị Phô mai bùng nổ
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TOO-PM')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TOO-PM', N'Snack que Toonies vị Phô mai bùng nổ 65g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 7000, 90, 20, 
            N'Snack que Toonies giòn rụm ngập tràn sốt phô mai béo ngậy món ăn vặt tuổi thơ.', N'images/products/toonies.jpg');
END

-- 36. CC-MUC: Snack mực sa tế Chop Chop
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CC-MUC')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CC-MUC', N'Snack mực lăn sa tế Chop Chop 50g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 7000, 85, 20, 
            N'Snack mực nướng sa tế Chop Chop giòn cay kích thích vị giác cực kỳ lôi cuốn.', N'images/products/chopchop.jpg');
END

-- 37. TY-X: Snack que TaYo! X vị Tảo ngọt
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TY-X')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TY-X', N'Snack que TaYo! X vị Tảo ngọt 60g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 8000, 70, 15, 
            N'Snack que rỗng TaYo! X vị tảo ngọt giòn xốp hương vị mới lạ hấp dẫn.', N'images/products/tayo-x.jpg');
END

-- 38. TY-TBK: Snack bánh gạo TaYo! vị cay Tokbokki
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TY-TBK')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TY-TBK', N'Snack bánh gạo TaYo! vị cay Tokbokki 60g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 8000, 65, 15, 
            N'Bánh gạo lắc vị Tokbokki Hàn Quốc cay ngọt giòn tan đậm đà.', N'images/products/tayo-tokbokki.jpg');
END

-- 39. CORN-01: Snack bắp nướng bơ Corn Chip
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CORN-01')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CORN-01', N'Snack bắp nướng bơ Corn Chip 56g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 7500, 70, 15, 
            N'Snack bắp nướng bơ sữa thơm ngậy giòn tan hương vị bắp ngọt tự nhiên.', N'images/products/cornchip.jpg');
END

-- 40. STING-01: Nước tăng lực Sting dâu lon 320ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'STING-01')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'STING-01', N'Nước tăng lực Sting hương dâu tây lon 320ml', N'Lon', N'Nước giải khát & Bia', 11000, 120, 24, 
            N'Nước tăng lực Sting dâu đỏ lon cao sảng khoái bổ sung năng lượng tức thì.', N'images/products/sting.jpg');
END

-- 41. OM-BO: Mì khoai tây Omachi xốt bò hầm gói 82g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OM-BO')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OM-BO', N'Mì khoai tây Omachi xốt bò hầm gói 82g', N'Gói', N'Thực phẩm & Gia vị', 9000, 150, 30, 
            N'Mì khoai tây Omachi sợi mì vàng dai mướt kết hợp xốt bò hầm thơm ngon không lo bị nóng.', N'images/products/omachi.jpg');
END

GO
