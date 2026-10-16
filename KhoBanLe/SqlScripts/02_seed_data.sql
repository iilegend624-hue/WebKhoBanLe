-- =========================================================================
-- ĐỒ ÁN CAPSTONE (ECO2415) - ĐỀ TÀI 13: KHO BÁN LẺ & BÁN HÀNG TẠI QUẦY (POS)
-- FILE: 02_seed_data.sql
-- MỤC TIÊU: Dữ liệu mẫu chuẩn logic cho SQL Server trùng khớp 100% với Codebase C#
-- =========================================================================

USE KhoBanLe;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
GO

-- 1. SEED DỮ LIỆU NHÀ CUNG CẤP (Supplier)
INSERT INTO dbo.Supplier (SupplierCode, Name, Phone, Email, Address, IsActive)
VALUES 
(N'NCC-TL', N'Công ty CP Tập đoàn Thiên Long', N'028 3750 5555', N'sales@thienlong.vn', N'Tầng 10, Sofic Tower, 10 Mai Chí Thọ, TP. Thủ Đức, TP.HCM', 1),
(N'NCC-VNM', N'Công ty CP Sữa Việt Nam (Vinamilk)', N'028 5415 5555', N'vinamilk@vinamilk.com.vn', N'Số 10 Tân Trào, Phường Tân Phú, Quận 7, TP.HCM', 1),
(N'NCC-AC', N'Công ty CP Acecook Việt Nam', N'028 3815 4064', N'info@acecookvietnam.com', N'Lô II-3, Đường số 11, KCN Tân Bình, P. Tây Thạnh, Tân Phú, TP.HCM', 1),
(N'NCC-KO', N'Công ty TNHH NGK Coca-Cola Việt Nam', N'028 3896 1000', N'cskh@coca-cola.com.vn', N'Xa lộ Hà Nội, P. Linh Trung, TP. Thủ Đức, TP.HCM', 1);

-- 2. SEED DỮ LIỆU SẢN PHẨM BÁN LẺ (Product - Trùng khớp 100% ProductRepository.cs)
INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
VALUES
(N'TL-01', N'Bút bi Thiên Long TL-027 xanh', N'Cái', N'Văn phòng phẩm', 5000, 5, 20, N'Bút bi Thiên Long mực xanh cao cấp ngòi 0.5mm êm trơn. (Demo Luật 1: Tồn 5)', N'images/products/thienlong.jpg'),
(N'HH-01', N'Mì Hảo Hảo Tôm chua cay (thùng 30 gói)', N'Thùng', N'Mì gói & Thực phẩm', 115000, 80, 25, N'Mì ăn liền Acecook Hảo Hảo vị Tôm chua cay truyền thống thùng 30 gói.', N'images/products/haohao.jpg'),
(N'VM-01', N'Sữa tươi tiệt trùng Vinamilk 100% 1L', N'Hộp', N'Sữa & Chế phẩm sữa', 34000, 35, 15, N'Sữa tươi tiệt trùng Vinamilk có đường 1 lít giàu canxi và vitamin D3.', N'images/products/vinamilk.jpg'),
(N'CC-01', N'Nước ngọt Coca-Cola lon 320ml (thùng 24 lon)', N'Thùng', N'Nước giải khát & Bia', 210000, 50, 20, N'Nước giải khát có ga Coca-Cola sảng khoái nguyên thùng 24 lon.', N'images/products/coca-cola.png'),
(N'ST25-05', N'Gạo thơm Sóc Trăng ST25 thượng hạng 5kg', N'Túi', N'Thực phẩm & Gia vị', 185000, 40, 10, N'Gạo ST25 gạo ngon nhất thế giới, hạt dài, dẻo thơm hảo hạng.', N'images/products/st25.jpg'),
(N'SP-01', N'Dầu ăn đậu nành Simply nguyên chất 1L', N'Chai', N'Thực phẩm & Gia vị', 58000, 30, 10, N'Dầu đậu nành nguyên chất 100% tốt cho tim mạch chai 1L.', N'images/products/simply.jpg'),
(N'SL-01', N'Nước rửa chén Sunlight Chanh túi 750g', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 32000, 6, 15, N'Nước rửa chén Sunlight tinh chất chanh tươi sạch dầu mỡ (Cảnh báo: Tồn 6)', N'images/products/sunlight.jpg'),
(N'PP-01', N'Khăn giấy lụa cao cấp Pulppy 10 cuộn', N'Lốc', N'Hóa mỹ phẩm & Đồ gia dụng', 85000, 0, 10, N'Khăn giấy lụa Pulppy mềm mại an toàn, thấm hút vượt trội (Hết hàng: Tồn 0)', N'images/products/pulppy.jpg'),
(N'CC-LON', N'Nước ngọt Coca-Cola vị nguyên bản lon 320ml', N'Lon', N'Nước giải khát & Bia', 11000, 150, 24, N'Nước giải khát có ga Coca-Cola sảng khoái lon lẻ 320ml bán chạy tại quầy.', N'images/products/coca-cola.png'),
(N'RB-01', N'Nước tăng lực Red Bull Thái lon 250ml', N'Lon', N'Nước giải khát & Bia', 15000, 120, 24, N'Nước tăng lực bò húc Red Bull nắp vàng Thái Lan chính hãng.', N'images/products/redbull.jpg'),
(N'RM-650', N'Dầu gội nam Romano Classic sạch gàu 650g', N'Chai', N'Hóa mỹ phẩm & Đồ gia dụng', 175000, 45, 10, N'Dầu gội nước hoa cao cấp Romano hương Classic lịch lãm, sạch gàu.', N'images/products/romano.jpg'),
(N'LAY-01', N'Snack khoai tây Lay''s vị tự nhiên Classic 95g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 22000, 85, 15, N'Bim bim khoai tây chiên vàng giòn rụm vị muối tự nhiên thơm ngon.', N'images/products/lays.jpg'),
(N'CP-12', N'Bánh ChocoPie Orion hộp 12 cái (396g)', N'Hộp', N'Bánh kẹo & Đồ ăn vặt', 56000, 60, 10, N'Bánh sôcôla ChocoPie kẹp kem dẻo marshmallow thơm lừng ngọt ngào.', N'images/products/chocopie.jpg'),
(N'LV-500', N'Nước khoáng thiên nhiên LaVie chai 500ml', N'Chai', N'Nước giải khát & Bia', 6000, 200, 48, N'Nước khoáng LaVie đóng chai tự nhiên thanh khiết cho sức khỏe.', N'images/products/lavie.jpg'),
(N'HN-330', N'Bia Heineken Silver lon 330ml', N'Lon', N'Nước giải khát & Bia', 21000, 95, 24, N'Bia Heineken Silver nhẹ êm dễ uống, hương vị tươi mát sảng khoái.', N'images/products/heineken.jpg'),
(N'VM-LOC', N'Sữa tươi Vinamilk có đường 180ml (lốc 4 hộp)', N'Lốc', N'Sữa & Chế phẩm sữa', 34000, 80, 20, N'Lốc 4 hộp sữa tươi tiệt trùng Vinamilk có đường giàu dinh dưỡng.', N'images/products/vinamilk.jpg'),
(N'SC-VM', N'Sữa chua ăn Vinamilk có đường (lốc 4 hộp x 100g)', N'Lốc', N'Sữa & Chế phẩm sữa', 28000, 70, 15, N'Sữa chua lên men tự nhiên bổ sung men vi sinh tốt cho hệ tiêu hóa.', N'images/products/yogurt.jpg'),
(N'OT-380', N'Sữa đặc có đường Ông Thọ đỏ lon 380g', N'Lon', N'Sữa & Chế phẩm sữa', 26000, 90, 20, N'Sữa đặc Ông Thọ sánh đặc ngọt béo dùng pha cà phê sữa thơm ngon.', N'images/products/ongtho.jpg'),
(N'HH-GOI', N'Mì Hảo Hảo tôm chua cay gói 75g', N'Gói', N'Mì gói & Thực phẩm', 4500, 300, 50, N'Mì gói Hảo Hảo tôm chua cay bán lẻ từng gói, món ăn quốc dân.', N'images/products/haohao.jpg'),
(N'NN-900', N'Nước mắm Nam Ngư Đệ Nhị chai 900ml', N'Chai', N'Thực phẩm & Gia vị', 32000, 65, 15, N'Nước mắm Nam Ngư Đệ Nhị vị ngon đậm đà, chấm hay nấu đều thơm.', N'images/products/namngu.jpg'),
(N'KN-400', N'Hạt nêm Knorr thịt thăn xương ống 400g', N'Gói', N'Thực phẩm & Gia vị', 39000, 55, 15, N'Hạt nêm Knorr chiết xuất thịt thăn xương ống cho canh ngọt thanh đậm đà.', N'images/products/knorr.jpg'),
(N'CS-250', N'Tương ớt Chinsu chai 250g', N'Chai', N'Thực phẩm & Gia vị', 14000, 70, 20, N'Tương ớt Chinsu thơm cay bùng nổ hương vị chấm món gì cũng ngon.', N'images/products/chinsu.jpg'),
(N'CG-180', N'Kem đánh răng Colgate Total than hoạt tính 180g', N'Hộp', N'Hóa mỹ phẩm & Đồ gia dụng', 45000, 50, 15, N'Kem đánh răng Colgate bảo vệ răng miệng toàn diện suốt 12h.', N'images/products/colgate.jpg'),
(N'OM-2KG', N'Bột giặt Omo Matic cửa trên túi 2kg', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 135000, 30, 10, N'Bột giặt Omo Matic xoáy bay vết bẩn cứng đầu, thơm lâu ngát hương.', N'images/products/omo.jpg'),
(N'CF-18', N'Nước xả vải Comfort một lần xả túi 1.8L', N'Túi', N'Hóa mỹ phẩm & Đồ gia dụng', 118000, 40, 10, N'Nước xả vải Comfort hương ban mai bền lâu giúp quần áo mềm mại.', N'images/products/comfort.jpg'),
(N'CC-PD', N'Kẹo dẻo Chupa Chups Panda vị trái cây 90g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 16000, 100, 20, N'Kẹo dẻo hình gấu Panda hương trái cây thơm ngon dai mềm vui nhộn.', N'images/products/chupachups.jpg'),
(N'CS-288', N'Bánh quy Cosy Marie Kinh Đô 288g', N'Gói', N'Bánh kẹo & Đồ ăn vặt', 38000, 45, 10, N'Bánh quy sữa Cosy Marie giòn xốp béo ngậy vị sữa tươi nguyên chất.', N'images/products/cosy.jpg'),
(N'CP-A5', N'Sổ tay Campus A5 kẻ ngang 200 trang', N'Quyển', N'Văn phòng phẩm', 25000, 60, 15, N'Sổ tay lò xo bìa cứng Campus giấy chống lóa viết êm không thấm mực.', N'images/products/campus.jpg'),
(N'C2-01', N'Trà xanh C2 hương chanh chai 455ml', N'Chai', N'Nước giải khát & Bia', 10000, 85, 24, N'Trà xanh đóng chai C2 ủ từ lá trà Thái Nguyên vị chanh thanh khiết.', N'images/products/c2.jpg');

-- 3. SEED DỮ LIỆU KHÁCH HÀNG (Customer - Dành cho POS & Khách Quen)
INSERT INTO dbo.Customer (CustomerCode, Name, Phone, Email, Address, Points)
VALUES
(N'KH-001', N'Khách mua lẻ tại quầy', N'0900 000 000', N'khachle@khobanle.vn', N'Mua trực tiếp tại quầy', 0),
(N'KH-002', N'Anh Hoàng Long', N'0903 123 456', N'hoanglong@gmail.com', N'120 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM', 120),
(N'KH-003', N'Chị Mai Phương', N'0918 654 321', N'maiphuong@gmail.com', N'45 Điện Biên Phủ, Phường 15, Bình Thạnh, TP.HCM', 450);

-- 4. SEED PHIẾU NHẬP KHO (GoodsReceipt & GoodsReceiptLine)
-- Phiếu PN-2026-001: Đã duyệt (Status = 1)
INSERT INTO dbo.GoodsReceipt (ReceiptNumber, SupplierId, ReceiptDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
VALUES (N'PN-2026-001', 3, DATEADD(day, -5, GETDATE()), N'Thủ kho Nguyễn Văn A', N'Quản lý Trần Thị B', DATEADD(day, -5, GETDATE()), 1, N'Nhập hàng định kỳ tháng 10');

DECLARE @ReceiptId1 INT = SCOPE_IDENTITY();
INSERT INTO dbo.GoodsReceiptLine (GoodsReceiptId, ProductId, Quantity, UnitPrice)
VALUES (@ReceiptId1, 2, 50, 105000);

-- Phiếu PN-2026-002: Đã duyệt (Status = 1)
INSERT INTO dbo.GoodsReceipt (ReceiptNumber, SupplierId, ReceiptDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
VALUES (N'PN-2026-002', 2, DATEADD(day, -2, GETDATE()), N'Thủ kho Nguyễn Văn A', N'Quản lý Trần Thị B', DATEADD(day, -2, GETDATE()), 1, N'Nhập bổ sung sữa tươi');

DECLARE @ReceiptId2 INT = SCOPE_IDENTITY();
INSERT INTO dbo.GoodsReceiptLine (GoodsReceiptId, ProductId, Quantity, UnitPrice)
VALUES (@ReceiptId2, 3, 20, 30000);

-- Phiếu PN-2026-003: Chờ duyệt (Status = 0)
INSERT INTO dbo.GoodsReceipt (ReceiptNumber, SupplierId, ReceiptDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
VALUES (N'PN-2026-003', 1, DATEADD(hour, -3, GETDATE()), N'Thủ kho Nguyễn Văn A', NULL, NULL, 0, N'Đơn nhập hàng văn phòng phẩm mới lập');

DECLARE @ReceiptId3 INT = SCOPE_IDENTITY();
INSERT INTO dbo.GoodsReceiptLine (GoodsReceiptId, ProductId, Quantity, UnitPrice)
VALUES (@ReceiptId3, 1, 25, 4200);

-- 5. SEED PHIẾU XUẤT KHO (GoodsIssue & GoodsIssueLine)
-- Phiếu PX-2026-001: Đã duyệt (Status = 1) - Xuất hàng bán lẻ tại quầy
INSERT INTO dbo.GoodsIssue (IssueNumber, CustomerOrDepartment, IssueDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
VALUES (N'PX-2026-001', N'Cửa hàng bán lẻ Chi nhánh 1', DATEADD(day, -3, GETDATE()), N'Thủ kho Nguyễn Văn A', N'Quản lý Trần Thị B', DATEADD(day, -3, GETDATE()), 1, N'Xuất hàng quầy bán lẻ chi nhánh 1');

DECLARE @IssueId1 INT = SCOPE_IDENTITY();
INSERT INTO dbo.GoodsIssueLine (GoodsIssueId, ProductId, Quantity, UnitPrice)
VALUES 
(@IssueId1, 2, 10, 115000),
(@IssueId1, 3, 5, 34000);

-- Phiếu PX-2026-002: Chờ duyệt (Status = 0) - DEMO LUẬT 1: Xuất 10 Bút bi TL trong khi tồn chỉ có 5
INSERT INTO dbo.GoodsIssue (IssueNumber, CustomerOrDepartment, IssueDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
VALUES (N'PX-2026-002', N'Bộ phận Hành chính - Văn phòng', DATEADD(hour, -4, GETDATE()), N'Thủ kho Nguyễn Văn A', NULL, NULL, 0, N'DEMO LUẬT 1: Yêu cầu xuất 10 chiếc Bút bi Thiên Long (Tồn chỉ có 5) -> Duyệt sẽ bị chặn!');

DECLARE @IssueId2 INT = SCOPE_IDENTITY();
INSERT INTO dbo.GoodsIssueLine (GoodsIssueId, ProductId, Quantity, UnitPrice)
VALUES (@IssueId2, 1, 10, 5000);

-- Phiếu PX-2026-003: Chờ duyệt (Status = 0) - Đơn xuất hợp lệ
INSERT INTO dbo.GoodsIssue (IssueNumber, CustomerOrDepartment, IssueDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
VALUES (N'PX-2026-003', N'Khách bán lẻ Đại lý An Bình', DATEADD(hour, -1, GETDATE()), N'Thủ kho Nguyễn Văn A', NULL, NULL, 0, N'Xuất hàng hợp lệ để kiểm tra duyệt thành công');

DECLARE @IssueId3 INT = SCOPE_IDENTITY();
INSERT INTO dbo.GoodsIssueLine (GoodsIssueId, ProductId, Quantity, UnitPrice)
VALUES (@IssueId3, 2, 5, 115000);

-- 6. SEED HÓA ĐƠN BÁN HÀNG TẠI QUẦY (PosOrder & PosOrderLine) - NỐI VỚI PHIẾU XUẤT KHO
-- Đơn POS-2026-001: Nối trực tiếp với Phiếu xuất PX-2026-001 (Đã thanh toán & trừ kho)
INSERT INTO dbo.PosOrder (OrderNumber, CustomerId, CustomerName, OrderDate, CashierName, PaymentMethod, SubTotal, DiscountAmount, TotalAmount, ReceivedAmount, ChangeAmount, GoodsIssueId, Status, Note)
VALUES (
    N'POS-2026-001', 
    1, 
    N'Khách mua lẻ tại quầy', 
    DATEADD(day, -3, GETDATE()), 
    N'Thu ngân Nguyễn Thị Mai', 
    N'Tiền mặt', 
    1320000, 
    0, 
    1320000, 
    1350000, 
    30000, 
    @IssueId1, 
    N'Paid', 
    N'Hóa đơn bán lẻ quầy thu ngân số 1, liên kết phiếu xuất kho PX-2026-001'
);

DECLARE @PosOrderId1 INT = SCOPE_IDENTITY();

-- Cập nhật liên kết ngược từ GoodsIssue về PosOrder
UPDATE dbo.GoodsIssue SET PosOrderId = @PosOrderId1 WHERE Id = @IssueId1;

INSERT INTO dbo.PosOrderLine (PosOrderId, ProductId, Quantity, UnitPrice)
VALUES 
(@PosOrderId1, 2, 10, 115000),
(@PosOrderId1, 3, 5, 34000);

-- Đơn POS-2026-002: Bán cho Anh Hoàng Long thanh toán qua VietQR
INSERT INTO dbo.PosOrder (OrderNumber, CustomerId, CustomerName, OrderDate, CashierName, PaymentMethod, SubTotal, DiscountAmount, TotalAmount, ReceivedAmount, ChangeAmount, GoodsIssueId, Status, Note)
VALUES (
    N'POS-2026-002', 
    2, 
    N'Anh Hoàng Long', 
    DATEADD(day, -1, GETDATE()), 
    N'Thu ngân Lê Văn Phát', 
    N'Chuyển khoản QR', 
    243000, 
    0, 
    243000, 
    243000, 
    0, 
    NULL, 
    N'Paid', 
    N'Thanh toán qua quét mã VietQR'
);

DECLARE @PosOrderId2 INT = SCOPE_IDENTITY();
INSERT INTO dbo.PosOrderLine (PosOrderId, ProductId, Quantity, UnitPrice)
VALUES 
(@PosOrderId2, 5, 1, 185000),
(@PosOrderId2, 6, 1, 58000);

PRINT N'Nạp dữ liệu mẫu hoàn tất 100%! Toàn bộ dữ liệu khớp chính xác tuyệt đối với C# Codebase và ERD!';
GO
