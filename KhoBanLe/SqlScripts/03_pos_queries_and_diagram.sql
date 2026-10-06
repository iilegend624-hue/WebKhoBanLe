-- =========================================================================
-- ĐỒ ÁN CAPSTONE (ECO2415) - ĐỀ TÀI 13: KHO BÁN LẺ & BÁN HÀNG TẠI QUẦY (POS)
-- FILE: 03_pos_queries_and_diagram.sql
-- MỤC TIÊU: 
-- 1. VIEW & STORED PROCEDURE BÁN HÀNG POS (Giao dịch Transaction an toàn & Trừ kho)
-- 2. TRUY VẤN NỐI SƠ ĐỒ THỰC THỂ (ERD JOINS & FOREIGN KEYS)
-- 3. ĐỐI SOÁT BẢO TOÀN TỒN KHO THEO 2 LUẬT NGHIỆP VỤ
-- =========================================================================

USE KhoBanLe;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- =========================================================================
-- 1. VIEW CHI TIẾT HÓA ĐƠN BÁN LẺ POS (Kết nối POS, Khách Hàng, Phiếu Xuất & Sản Phẩm)
-- =========================================================================
IF OBJECT_ID('dbo.v_PosOrderDetails', 'V') IS NOT NULL DROP VIEW dbo.v_PosOrderDetails;
GO

CREATE VIEW dbo.v_PosOrderDetails
AS
SELECT 
    o.Id AS PosOrderId,
    o.OrderNumber,
    o.OrderDate,
    o.CashierName,
    o.PaymentMethod,
    ISNULL(c.CustomerCode, N'VÃNG LAI') AS CustomerCode,
    ISNULL(c.Name, o.CustomerName) AS CustomerName,
    ISNULL(c.Phone, N'N/A') AS CustomerPhone,
    o.GoodsIssueId,
    gi.IssueNumber,
    p.Sku AS ProductSku,
    p.Name AS ProductName,
    p.Unit AS ProductUnit,
    p.Category AS ProductCategory,
    ol.Quantity,
    ol.UnitPrice,
    ol.LineTotal,
    o.TotalAmount AS OrderTotalAmount,
    o.ReceivedAmount,
    o.ChangeAmount,
    o.Status AS OrderStatus
FROM dbo.PosOrder o
LEFT JOIN dbo.Customer c ON o.CustomerId = c.Id
LEFT JOIN dbo.GoodsIssue gi ON o.GoodsIssueId = gi.Id
INNER JOIN dbo.PosOrderLine ol ON o.Id = ol.PosOrderId
INNER JOIN dbo.Product p ON ol.ProductId = p.Id;
GO

-- =========================================================================
-- 2. VIEW BÁO CÁO DOANH THU & XUẤT KHO THEO NGÀY
-- =========================================================================
IF OBJECT_ID('dbo.v_DailyPosRevenue', 'V') IS NOT NULL DROP VIEW dbo.v_DailyPosRevenue;
GO

CREATE VIEW dbo.v_DailyPosRevenue
AS
SELECT 
    CAST(o.OrderDate AS DATE) AS SaleDate,
    COUNT(DISTINCT o.Id) AS TotalOrders,
    SUM(ol.Quantity) AS TotalItemsSold,
    SUM(o.TotalAmount) AS TotalRevenue,
    SUM(CASE WHEN o.PaymentMethod = N'Tiền mặt' THEN o.TotalAmount ELSE 0 END) AS CashRevenue,
    SUM(CASE WHEN o.PaymentMethod = N'Chuyển khoản QR' THEN o.TotalAmount ELSE 0 END) AS QrRevenue,
    SUM(CASE WHEN o.PaymentMethod = N'Quẹt thẻ POS' THEN o.TotalAmount ELSE 0 END) AS CardRevenue
FROM dbo.PosOrder o
INNER JOIN dbo.PosOrderLine ol ON o.Id = ol.PosOrderId
WHERE o.Status = N'Paid'
GROUP BY CAST(o.OrderDate AS DATE);
GO

-- =========================================================================
-- 3. STORED PROCEDURE BÁN HÀNG TẠI QUẦY (POS CHECKOUT)
-- Nghiệp vụ chuẩn: Kiểm tra tồn kho -> Tạo GoodsIssue -> Duyệt trừ kho -> Tạo PosOrder trong 1 TRANSACTION
-- =========================================================================
IF OBJECT_ID('dbo.sp_ProcessPosCheckout', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_ProcessPosCheckout;
GO

CREATE PROCEDURE dbo.sp_ProcessPosCheckout
    @CustomerName NVARCHAR(200) = N'Khách mua lẻ tại quầy',
    @CustomerId INT = NULL,
    @CashierName NVARCHAR(100) = N'Thu ngân POS',
    @PaymentMethod NVARCHAR(50) = N'Tiền mặt',
    @ProductId INT,
    @Quantity INT,
    @ReceivedAmount DECIMAL(18,2) = 0,
    @NewOrderNumber NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Kiểm tra tồn kho (LUẬT 1: Chặn xuất âm)
        DECLARE @CurrentStock INT, @Price DECIMAL(18,2), @ProductName NVARCHAR(200);
        SELECT @CurrentStock = CurrentStock, @Price = Price, @ProductName = Name
        FROM dbo.Product WITH (UPDLOCK, ROWLOCK)
        WHERE Id = @ProductId;

        IF @CurrentStock IS NULL
        BEGIN
            RAISERROR(N'Sản phẩm không tồn tại trong hệ thống!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @CurrentStock < @Quantity
        BEGIN
            DECLARE @ErrMsg NVARCHAR(500) = FORMATMESSAGE(N'Hàng "%s" chỉ còn tồn %d, không đủ để bán %d!', @ProductName, @CurrentStock, @Quantity);
            RAISERROR(@ErrMsg, 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 2. Tạo Phiếu xuất kho bán lẻ (GoodsIssue)
        DECLARE @IssueNum NVARCHAR(50) = N'PX-' + CONVERT(NVARCHAR(4), YEAR(GETDATE())) + N'-' + RIGHT(N'000' + CAST(NEXT VALUE FOR dbo.Seq_IssueNumber AS NVARCHAR(10)), 3);
        -- Nếu chưa có sequence, sinh tự động từ Count + 1
        SET @IssueNum = N'PX-POS-' + FORMAT(GETDATE(), 'yyyyMMdd-HHmmss');

        INSERT INTO dbo.GoodsIssue (IssueNumber, CustomerOrDepartment, IssueDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
        VALUES (@IssueNum, @CustomerName, GETDATE(), @CashierName, N'Hệ thống POS', GETDATE(), 1, N'Tự động xuất kho từ đơn bán lẻ POS');

        DECLARE @IssueId INT = SCOPE_IDENTITY();

        INSERT INTO dbo.GoodsIssueLine (GoodsIssueId, ProductId, Quantity, UnitPrice)
        VALUES (@IssueId, @ProductId, @Quantity, @Price);

        -- 3. Trừ tồn kho sản phẩm (Luật 1: Duyệt mới đổi tồn)
        UPDATE dbo.Product
        SET CurrentStock = CurrentStock - @Quantity
        WHERE Id = @ProductId;

        -- 4. Tạo Hóa Đơn Bán Lẻ POS (PosOrder)
        DECLARE @OrderNum NVARCHAR(50) = N'POS-' + FORMAT(GETDATE(), 'yyyyMMdd-HHmmss');
        SET @NewOrderNumber = @OrderNum;

        DECLARE @TotalAmount DECIMAL(18,2) = @Quantity * @Price;
        DECLARE @ChangeAmount DECIMAL(18,2) = CASE WHEN @ReceivedAmount > @TotalAmount THEN @ReceivedAmount - @TotalAmount ELSE 0 END;

        INSERT INTO dbo.PosOrder (OrderNumber, CustomerId, CustomerName, OrderDate, CashierName, PaymentMethod, SubTotal, DiscountAmount, TotalAmount, ReceivedAmount, ChangeAmount, GoodsIssueId, Status, Note)
        VALUES (@OrderNum, @CustomerId, @CustomerName, GETDATE(), @CashierName, @PaymentMethod, @TotalAmount, 0, @TotalAmount, @ReceivedAmount, @ChangeAmount, @IssueId, N'Paid', N'Thanh toán tại quầy qua SP');

        DECLARE @PosOrderId INT = SCOPE_IDENTITY();

        INSERT INTO dbo.PosOrderLine (PosOrderId, ProductId, Quantity, UnitPrice)
        VALUES (@PosOrderId, @ProductId, @Quantity, @Price);

        -- 5. Cập nhật liên kết ngược
        UPDATE dbo.GoodsIssue SET PosOrderId = @PosOrderId WHERE Id = @IssueId;

        COMMIT TRANSACTION;
        PRINT N'Giao dịch bán lẻ POS hoàn tất thành công! Đã trừ tồn kho và tạo chứng từ liên kết.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- =========================================================================
-- 4. TRUY VẤN KIỂM TRA MỐI QUAN HỆ KHÓA NGOẠI (ERD INTEGRITY CHECK)
-- Giảng viên / Ban giám khảo kiểm tra toàn bộ 10 liên kết khóa ngoại trong CSDL
-- =========================================================================
SELECT 
    fk.name AS ForeignKeyConstraint,
    tp.name AS ParentTable,
    cp.name AS ParentColumn,
    tr.name AS ReferencedTable,
    cr.name AS ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.tables tp ON fkc.parent_object_id = tp.object_id
INNER JOIN sys.columns cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
INNER JOIN sys.tables tr ON fkc.referenced_object_id = tr.object_id
INNER JOIN sys.columns cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
ORDER BY tp.name, fk.name;
GO

-- =========================================================================
-- 5. TRUY VẤN KIỂM CHỨNG LUẬT 2: BẢO TOÀN CÔNG THỨC NHẬP - XUẤT - TỒN
-- Tồn Hiện Tại = Tồn Đầu Kỳ + Tổng Nhập (Approved) - Tổng Xuất (Approved)
-- =========================================================================
SELECT 
    p.Sku,
    p.Name AS ProductName,
    p.Unit,
    ISNULL(r.TotalImport, 0) AS TotalApprovedImport,
    ISNULL(i.TotalExport, 0) AS TotalApprovedExport,
    p.CurrentStock AS EndingStock,
    (p.CurrentStock - ISNULL(r.TotalImport, 0) + ISNULL(i.TotalExport, 0)) AS BeginningStock_Calculated
FROM dbo.Product p
LEFT JOIN (
    SELECT rl.ProductId, SUM(rl.Quantity) AS TotalImport
    FROM dbo.GoodsReceiptLine rl
    INNER JOIN dbo.GoodsReceipt r ON rl.GoodsReceiptId = r.Id
    WHERE r.Status = 1 -- Đã duyệt
    GROUP BY rl.ProductId
) r ON p.Id = r.ProductId
LEFT JOIN (
    SELECT il.ProductId, SUM(il.Quantity) AS TotalExport
    FROM dbo.GoodsIssueLine il
    INNER JOIN dbo.GoodsIssue i ON il.GoodsIssueId = i.Id
    WHERE i.Status = 1 -- Đã duyệt
    GROUP BY il.ProductId
) i ON p.Id = i.ProductId;
GO
