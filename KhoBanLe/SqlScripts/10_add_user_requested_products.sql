-- =========================================================================
-- SCRIPT 10: THÊM CÁC MẶT HÀNG THEO YÊU CẦU CỦA NGƯỜI DÙNG:
-- 1. Cà phê gói (G7 bịch lớn, Nescafé đen đá, Vinacafé CHẤT)
-- 2. Bia Huda (Bia Huda lon 330ml, Thùng Huda 24 lon, Huda Gold)
-- 3. Một số loại kẹo (Kẹo Kopiko, Alpenliebe Caramen, Kẹo dẻo Haribo, Kẹo Dynamite)
-- 4. Văn phòng phẩm (Hộp ghim bấm số 10, Máy bấm kim số 10, Giấy Double A, Bút dạ quang...)
-- 5. Sửa link ảnh chuẩn cho các loại cà phê cũ (TN-CHUS, NES-3IN1, G7-3IN1)
-- =========================================================================

-- Sửa ảnh cà phê cũ không bị nhầm sang chai trà xanh C2
UPDATE dbo.Product SET ImageUrl = N'images/products/trungnguyen-chus.jpg' WHERE Sku = N'TN-CHUS';
UPDATE dbo.Product SET ImageUrl = N'images/products/nescafe-3in1.jpg' WHERE Sku = N'NES-3IN1';
UPDATE dbo.Product SET ImageUrl = N'images/products/g7-3in1.jpg' WHERE Sku = N'G7-3IN1';

-- 1. CÀ PHÊ GÓI
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'G7-50')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'G7-50', N'Cà phê hòa tan G7 3in1 Trung Nguyên bịch 50 gói x 16g', N'Bịch', N'Cà phê & Trà', 135000, 35, 10, N'Cà phê hòa tan G7 hương vị đậm đà nguyên bản từ hạt cà phê Buôn Ma Thuột hảo hạng.', N'images/products/g7-3in1.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'NES-DEN')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'NES-DEN', N'Cà phê đen hòa tan Nescafé Café Việt đậm đà hộp 15 gói', N'Hộp', N'Cà phê & Trà', 48000, 25, 10, N'Cà phê đen đá chuẩn gu Việt, vị đắng đậm đà và hương thơm nồng nàn.', N'images/products/nescafe-3in1.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'VINA-CHAT')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'VINA-CHAT', N'Cà phê sữa hòa tan Vinacafé CHẤT Sài Gòn hộp 10 gói', N'Hộp', N'Cà phê & Trà', 42000, 30, 10, N'Cà phê sữa đá Sài Gòn thơm béo ngậy, chuẩn gu người tiêu dùng Việt.', N'images/products/trungnguyen-chus.jpg', SYSDATETIME());

-- 2. BIA HUDA
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HUDA-330')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HUDA-330', N'Bia Huda lon 330ml đậm tình miền Trung', N'Lon', N'Nước giải khát & Bia', 14000, 72, 24, N'Bia Huda đặc sản xứ Huế được nấu từ lúa mạch thượng hạng và hoa bia chất lượng châu Âu.', N'images/products/huda-beer.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HUDA-T24')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HUDA-T24', N'Bia Huda lon 330ml (Thùng 24 lon)', N'Thùng', N'Nước giải khát & Bia', 315000, 40, 15, N'Thùng 24 lon bia Huda 330ml mát lạnh sảng khoái, lựa chọn số 1 cho các bữa tiệc.', N'images/products/huda-beer.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HUDA-GOLD')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HUDA-GOLD', N'Bia Huda Gold lon cao 330ml thượng hạng', N'Lon', N'Nước giải khát & Bia', 18000, 50, 15, N'Dòng bia cao cấp Huda Gold với hương thơm hoa bia tinh tế và hậu vị êm đằm.', N'images/products/huda-beer.jpg', SYSDATETIME());

-- 3. CÁC LOẠI KẸO
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'KOP-140')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'KOP-140', N'Kẹo cà phê Kopiko hương vị cà phê đậm đà gói 140g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 18000, 60, 20, N'Kẹo ngậm chiết xuất từ hạt cà phê thật Kopiko giúp tỉnh táo sảng khoái mọi lúc.', N'images/products/kopiko.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'ALP-CARA')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'ALP-CARA', N'Kẹo mềm sữa Caramen Alpenliebe béo ngậy gói 120g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 16000, 55, 15, N'Kẹo mềm sữa caramen Alpenliebe ngọt ngào thơm béo tan chảy trong miệng.', N'images/products/alpenliebe-dau.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'HAR-BEAR')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'HAR-BEAR', N'Kẹo dẻo gấu Haribo Goldbears nhập khẩu gói 80g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 26000, 45, 10, N'Kẹo dẻo hình chú gấu nhiều hương vị trái cây tự nhiên được trẻ em yêu thích.', N'images/products/chupachups-panda.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'DYN-MINT')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'DYN-MINT', N'Kẹo bạc hà nhân socola Dynamite BigBang gói 125g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 14000, 50, 15, N'Sự kết hợp bùng nổ giữa vị bạc hà the mát bên ngoài và nhân socola đậm đà bên trong.', N'images/products/coolair-58g.jpg', SYSDATETIME());

-- 4. GHIM BẤM VÀ ĐỒ DÙNG VĂN PHÒNG PHẨM
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'GHIM-10')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'GHIM-10', N'Hộp ghim bấm số 10 Deli Plus mạ kẽm (1000 ghim)', N'Hộp', N'Văn phòng phẩm', 4000, 120, 30, N'Hộp kim bấm số 10 chống rỉ sét bấm chắc chắn không gãy thích hợp cho văn phòng và học tập.', N'images/products/ghim-bam-no10.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'DAP-10')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'DAP-10', N'Máy dập ghim / Bấm kim số 10 Deli trợ lực kèm gỡ ghim', N'Cái', N'Văn phòng phẩm', 28000, 35, 10, N'Dập ghim nhỏ gọn tiện lợi bấm nhẹ nhàng có khay chứa ghim dự phòng và móc gỡ ghim.', N'images/products/dap-ghim-no10.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'DA-A4-70')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'DA-A4-70', N'Giấy in A4 Double A 70gsm cao cấp (Ram 500 tờ)', N'Ram', N'Văn phòng phẩm', 72000, 40, 15, N'Giấy in photo Double A độ trắng mịn cao không kẹt giấy cho bản in sắc nét.', N'images/products/double-a-a4.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'BK-18')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'BK-18', N'Băng keo trong văn phòng 1.8cm cuộn 100 yard độ dính cao', N'Cuộn', N'Văn phòng phẩm', 8000, 65, 20, N'Băng dính trong suốt bề rộng 1.8cm keo bám dính chắc không ố vàng.', N'images/products/campus.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'KEO-18')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'KEO-18', N'Kéo văn phòng Deli cán bọc nhựa êm tay lưỡi inox 18cm', N'Cái', N'Văn phòng phẩm', 22000, 25, 10, N'Kéo lưỡi thép không gỉ sắc bén cán nhựa cao cấp trợ lực cắt ngọt.', N'images/products/campus-a5.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'BUT-DA')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'BUT-DA', N'Bút dạ quang nhớ dòng Thiên Long pastel 5 màu nổi bật', N'Cây', N'Văn phòng phẩm', 11000, 50, 15, N'Bút dạ quang màu mực tươi sáng không lem mực bảo vệ mắt khi đọc tài liệu.', N'images/products/thienlong-tl027.jpg', SYSDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CHI-2B')
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt)
    VALUES (N'CHI-2B', N'Bút chì gỗ 2B Thiên Long kèm gôm tẩy (Hộp 12 cây)', N'Hộp', N'Văn phòng phẩm', 36000, 30, 10, N'Bút chì ruột 2B đậm nét ngòi chì mềm êm dễ gọt dùng vẽ và ghi chép.', N'images/products/thienlong.jpg', SYSDATETIME());
