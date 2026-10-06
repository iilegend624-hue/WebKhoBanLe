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

-- 2. SEED DỮ LIỆU SẢN PHẨM (Product - Trùng khớp 100% ProductRepository.cs)
INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
VALUES
(N'TL-01', N'Bút bi Thiên Long TL-027', N'Cái', N'Văn phòng phẩm', 5000, 5, 20, N'Bút bi Thiên Long mực xanh cao cấp ngòi 0.5mm êm trơn. (Dùng demo Luật 1: Tồn 5)', N'https://images.unsplash.com/photo-1583485088034-697b5bc54ccd?w=300'),
(N'HH-01', N'Mì Hảo Hảo Tôm chua cay (30 gói)', N'Thùng', N'Thực phẩm', 115000, 80, 25, N'Mì ăn liền Acecook Hảo Hảo vị Tôm chua cay truyền thống.', N'https://images.unsplash.com/photo-1612927601601-6638404737ce?w=300'),
(N'VM-01', N'Sữa tươi tiệt trùng Vinamilk 100% 1L', N'Hộp', N'Sữa & Đồ uống', 34000, 35, 15, N'Sữa tươi tiệt trùng Vinamilk có đường 1 lít giàu canxi và vitamin.', N'https://images.unsplash.com/photo-1550583724-b2692b85b150?w=300'),
(N'CC-01', N'Nước ngọt Coca-Cola lon 330ml (24 lon)', N'Thùng', N'Sữa & Đồ uống', 210000, 50, 20, N'Nước giải khát có ga Coca-Cola sảng khoái.', N'https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=300'),
(N'ST25-05', N'Gạo thơm Sóc Trăng ST25 thượng hạng 5kg', N'Túi', N'Thực phẩm', 185000, 40, 10, N'Gạo ST25 gạo ngon nhất thế giới, hạt dài, dẻo thơm.', N'https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300'),
(N'SP-01', N'Dầu ăn đậu nành Simply nguyên chất 1L', N'Chai', N'Thực phẩm', 58000, 30, 10, N'Dầu đậu nành nguyên chất 100% tốt cho tim mạch.', N'https://images.unsplash.com/photo-1474979266404-7eaacbcd87c5?w=300'),
(N'SL-01', N'Nước rửa chén Sunlight Chanh túi 750g', N'Túi', N'Hóa mỹ phẩm', 32000, 6, 15, N'Nước rửa chén sạch dầu mỡ (Cảnh báo sắp hết hàng: 6 < 15)', N'https://images.unsplash.com/photo-1585421514284-efb74c2b69ba?w=300'),
(N'PP-01', N'Khăn giấy lụa cao cấp Pulppy 10 cuộn', N'Lốc', N'Đồ gia dụng - Sinh hoạt', 85000, 0, 10, N'Khăn giấy Pulppy mềm mại (Cảnh báo hết hàng: Tồn 0)', N'https://images.unsplash.com/photo-1584556812952-905ffd0c611a?w=300'),
(N'EGO-HELIOS', N'Ego Helios', N'Gói', N'Thời trang - Trang sức', 1800000, 45, 10, N'Giao diện web bán hàng Thời trang & Đồng hồ cao cấp, phong cách sang trọng hiện đại.', N'https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=600'),
(N'EGO-FITNESS', N'Ego Fitness', N'Gói', N'Thể thao - Dịch vụ', 1900000, 32, 8, N'Giao diện thể thao, phòng gym, thực phẩm thể hình năng động và bứt phá doanh số.', N'https://images.unsplash.com/photo-1517838277536-f5f99be501cd?w=600'),
(N'HER-COSMETIC', N'Her Cosmetic', N'Gói', N'Mỹ phẩm - Làm đẹp', 1840000, 50, 12, N'Giao diện mỹ phẩm, làm đẹp, spa sắc sảo và thanh lịch tối ưu tỷ lệ chuyển đổi.', N'https://images.unsplash.com/photo-1596462502278-27bfdc403348?w=600'),
(N'ORGANIC-FOOD', N'Thực Phẩm Hữu Cơ Giàu Dinh Dưỡng', N'Combo', N'Thực phẩm - Nhà hàng', 1200000, 28, 10, N'Giao diện cửa hàng thực phẩm sạch hữu cơ, rau củ quả tươi sống, chuẩn SEO.', N'https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600'),
(N'COZY-HOME', N'Nội Thất Cozy Home & Decor', N'Gói', N'Nội thất - Trang trí', 1500000, 20, 5, N'Mẫu giao diện nội thất gia đình, kiến trúc và trang trí nhà cửa cao cấp.', N'https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=600'),
(N'SMART-TECH', N'SmartTech Điện Tử Số', N'Gói', N'Công nghệ - Kỹ thuật số', 2100000, 15, 5, N'Cửa hàng điện thoại, máy tính, linh kiện điện tử hiện đại và mạnh mẽ.', N'https://images.unsplash.com/photo-1519389950473-47ba0277781c?w=600'),
(N'NANO-LIGHT', N'Thiết Bị Điện Gia Đình NanoLight', N'Gói', N'Thiết bị điện', 1450000, 25, 8, N'Thiết bị chiếu sáng, điện dân dụng, giải pháp điện thông minh.', N'https://images.unsplash.com/photo-1558002038-1055907df827?w=600'),
(N'AUTO-PRO', N'Phụ Kiện Ô Tô - Xe Máy Sport', N'Gói', N'Ô tô - Xe máy', 2500000, 18, 5, N'Giao diện gara, đồ chơi ô tô xe máy, phụ tùng xe phân khối lớn.', N'https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=600'),
(N'CRAFT-GIFT', N'Thủ Công Mỹ Nghệ & Quà Tặng Tinh Xảo', N'Gói', N'Thủ công - Mỹ nghệ - Quà tặng', 680000, 30, 10, N'Đồ lưu niệm, gốm sứ Bát Tràng, tranh thêu và quà tặng thủ công truyền thống.', N'https://images.unsplash.com/photo-1513519245088-0e12902e5a38?w=600'),
(N'BLUE-SKY', N'Khách Sạn & Du Lịch BlueSky Resort', N'Gói', N'Khách sạn - Du lịch', 2200000, 12, 4, N'Đặt phòng khách sạn, tour du lịch, nghỉ dưỡng và vé máy bay trực tuyến.', N'https://images.unsplash.com/photo-1566073771259-6a8506099945?w=600'),
(N'PRIME-LAND', N'Bất Động Sản Đất Vàng Prime Land', N'Gói', N'Bất động sản - Xây dựng', 2800000, 10, 3, N'Dự án căn hộ, biệt thự nghỉ dưỡng, sàn giao dịch bất động sản chuyên nghiệp.', N'https://images.unsplash.com/photo-1560518883-ce09059eeffa?w=600'),
(N'PRO-BIZ', N'Hồ Sơ Doanh Nghiệp ProBiz', N'Gói', N'Doanh nghiệp', 1950000, 22, 6, N'Trang giới thiệu công ty, tập đoàn đa ngành, hồ sơ năng lực uy tín.', N'https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=600');

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
