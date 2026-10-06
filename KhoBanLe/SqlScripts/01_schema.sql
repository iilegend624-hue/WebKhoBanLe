-- =========================================================================
-- ĐỒ ÁN CAPSTONE (ECO2415) - ĐỀ TÀI 13: KHO BÁN LẺ & BÁN HÀNG TẠI QUẦY (POS)
-- FILE: 01_schema.sql
-- MỤC TIÊU: Thiết kế CSDL Chuẩn 3NF trên SQL Server (SSMS) tích hợp POS
-- Nối toàn bộ Sơ đồ Thực thể (ERD) và Mối quan hệ Khóa ngoại (FK) chặt chẽ
-- =========================================================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'KhoBanLe')
BEGIN
    CREATE DATABASE KhoBanLe;
END
GO

USE KhoBanLe;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- XÓA RÀNG BUỘC KHÓA NGOẠI NẾU CÓ ĐỂ TRÁNH XUNG ĐỘT KHI DROP
IF OBJECT_ID('dbo.FK_GoodsIssue_PosOrder', 'F') IS NOT NULL 
    ALTER TABLE dbo.GoodsIssue DROP CONSTRAINT FK_GoodsIssue_PosOrder;
IF OBJECT_ID('dbo.FK_PosOrder_GoodsIssue', 'F') IS NOT NULL 
    ALTER TABLE dbo.PosOrder DROP CONSTRAINT FK_PosOrder_GoodsIssue;
GO

-- XÓA BẢNG CŨ THEO THỨ TỰ RÀNG BUỘC KHÓA NGOẠI (NẾU ĐÃ TỒN TẠI)
IF OBJECT_ID('dbo.PosOrderLine', 'U') IS NOT NULL DROP TABLE dbo.PosOrderLine;
IF OBJECT_ID('dbo.PosOrder', 'U') IS NOT NULL DROP TABLE dbo.PosOrder;
IF OBJECT_ID('dbo.GoodsReceiptLine', 'U') IS NOT NULL DROP TABLE dbo.GoodsReceiptLine;
IF OBJECT_ID('dbo.GoodsReceipt', 'U') IS NOT NULL DROP TABLE dbo.GoodsReceipt;
IF OBJECT_ID('dbo.GoodsIssueLine', 'U') IS NOT NULL DROP TABLE dbo.GoodsIssueLine;
IF OBJECT_ID('dbo.GoodsIssue', 'U') IS NOT NULL DROP TABLE dbo.GoodsIssue;
IF OBJECT_ID('dbo.Customer', 'U') IS NOT NULL DROP TABLE dbo.Customer;
IF OBJECT_ID('dbo.Product', 'U') IS NOT NULL DROP TABLE dbo.Product;
IF OBJECT_ID('dbo.Supplier', 'U') IS NOT NULL DROP TABLE dbo.Supplier;
GO

-- =========================================================================
-- 1. BẢNG NHÀ CUNG CẤP (Supplier)
-- =========================================================================
CREATE TABLE dbo.Supplier (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SupplierCode NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(300) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =========================================================================
-- 2. BẢNG HÀNG HÓA BÁN LẺ (Product)
-- =========================================================================
CREATE TABLE dbo.Product (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Sku NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Unit NVARCHAR(50) NOT NULL DEFAULT N'Cái',
    Category NVARCHAR(100) NOT NULL DEFAULT N'Bách hóa',
    Price DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (Price >= 0),
    CurrentStock INT NOT NULL DEFAULT 0 CHECK (CurrentStock >= 0),
    MinStockLevel INT NOT NULL DEFAULT 10 CHECK (MinStockLevel >= 0),
    Description NVARCHAR(500) NULL,
    ImageUrl NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =========================================================================
-- 3. BẢNG KHÁCH HÀNG (Customer) - Bổ sung cho Quầy Bán Lẻ POS & Khách Thân Thiết
-- =========================================================================
CREATE TABLE dbo.Customer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerCode NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(300) NULL,
    Points INT NOT NULL DEFAULT 0 CHECK (Points >= 0),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =========================================================================
-- 4. BẢNG HEADER PHIẾU NHẬP KHO (GoodsReceipt)
-- Quan hệ: Supplier (1) ---> (N) GoodsReceipt
-- =========================================================================
CREATE TABLE dbo.GoodsReceipt (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReceiptNumber NVARCHAR(50) NOT NULL UNIQUE,
    SupplierId INT NOT NULL,
    ReceiptDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT N'Thủ kho',
    ApprovedBy NVARCHAR(100) NULL,
    ApprovedDate DATETIME2 NULL,
    Status INT NOT NULL DEFAULT 0, -- 0: Draft (Chờ duyệt), 1: Approved (Đã duyệt), 2: Cancelled (Đã hủy)
    Note NVARCHAR(500) NULL,
    CONSTRAINT FK_GoodsReceipt_Supplier FOREIGN KEY (SupplierId) REFERENCES dbo.Supplier(Id)
);
GO

-- =========================================================================
-- 5. BẢNG CHI TIẾT DÒNG NHẬP KHO (GoodsReceiptLine)
-- Quan hệ: GoodsReceipt (1) ---> (N) GoodsReceiptLine; Product (1) ---> (N) GoodsReceiptLine
-- =========================================================================
CREATE TABLE dbo.GoodsReceiptLine (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GoodsReceiptId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL CHECK (UnitPrice >= 0),
    LineTotal AS (Quantity * UnitPrice) PERSISTED,
    CONSTRAINT FK_ReceiptLine_Receipt FOREIGN KEY (GoodsReceiptId) REFERENCES dbo.GoodsReceipt(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ReceiptLine_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(Id)
);
GO

-- =========================================================================
-- 6. BẢNG HEADER PHIẾU XUẤT KHO (GoodsIssue)
-- Tuân thủ LUẬT 1: Chặn xuất âm kho, chỉ xuất khi Status = 1 (Approved)
-- =========================================================================
CREATE TABLE dbo.GoodsIssue (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IssueNumber NVARCHAR(50) NOT NULL UNIQUE,
    CustomerOrDepartment NVARCHAR(200) NOT NULL,
    IssueDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT N'Thủ kho',
    ApprovedBy NVARCHAR(100) NULL,
    ApprovedDate DATETIME2 NULL,
    Status INT NOT NULL DEFAULT 0, -- 0: Draft, 1: Approved, 2: Cancelled
    Note NVARCHAR(500) NULL,
    PosOrderId INT NULL -- Khóa ngoại tùy chọn liên kết ngược tới đơn POS (nếu xuất từ bán lẻ)
);
GO

-- =========================================================================
-- 7. BẢNG CHI TIẾT DÒNG XUẤT KHO (GoodsIssueLine)
-- Quan hệ: GoodsIssue (1) ---> (N) GoodsIssueLine; Product (1) ---> (N) GoodsIssueLine
-- =========================================================================
CREATE TABLE dbo.GoodsIssueLine (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GoodsIssueId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL CHECK (UnitPrice >= 0),
    LineTotal AS (Quantity * UnitPrice) PERSISTED,
    CONSTRAINT FK_IssueLine_Issue FOREIGN KEY (GoodsIssueId) REFERENCES dbo.GoodsIssue(Id) ON DELETE CASCADE,
    CONSTRAINT FK_IssueLine_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(Id)
);
GO

-- =========================================================================
-- 8. BẢNG HÓA ĐƠN BÁN HÀNG TẠI QUẦY (PosOrder) - BỔ SUNG POS
-- Nối quan hệ:
--   - Customer (1) ---> (N) PosOrder (Khách hàng lẻ / Khách quen)
--   - GoodsIssue (1) ---> (0..1) PosOrder (Liên kết Phiếu xuất kho bán lẻ để trừ tồn kho thực tế)
-- =========================================================================
CREATE TABLE dbo.PosOrder (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderNumber NVARCHAR(50) NOT NULL UNIQUE,
    CustomerId INT NULL,
    CustomerName NVARCHAR(200) NOT NULL DEFAULT N'Khách mua lẻ tại quầy',
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CashierName NVARCHAR(100) NOT NULL DEFAULT N'Thu ngân / Bán lẻ POS',
    PaymentMethod NVARCHAR(50) NOT NULL DEFAULT N'Tiền mặt', -- Tiền mặt, Chuyển khoản QR, Quẹt thẻ POS
    SubTotal DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (SubTotal >= 0),
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (DiscountAmount >= 0),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (TotalAmount >= 0),
    ReceivedAmount DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (ReceivedAmount >= 0),
    ChangeAmount DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (ChangeAmount >= 0),
    GoodsIssueId INT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Paid', -- Paid (Đã thanh toán), Cancelled (Đã hủy)
    Note NVARCHAR(500) NULL,
    CONSTRAINT FK_PosOrder_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id),
    CONSTRAINT FK_PosOrder_GoodsIssue FOREIGN KEY (GoodsIssueId) REFERENCES dbo.GoodsIssue(Id)
);
GO

-- Bổ sung ràng buộc khóa ngoại từ GoodsIssue về PosOrder (liên kết 2 chiều an toàn)
ALTER TABLE dbo.GoodsIssue
ADD CONSTRAINT FK_GoodsIssue_PosOrder FOREIGN KEY (PosOrderId) REFERENCES dbo.PosOrder(Id);
GO

-- =========================================================================
-- 9. BẢNG CHI TIẾT HÓA ĐƠN BÁN HÀNG TẠI QUẦY (PosOrderLine) - BỔ SUNG POS
-- Quan hệ: PosOrder (1) ---> (N) PosOrderLine; Product (1) ---> (N) PosOrderLine
-- =========================================================================
CREATE TABLE dbo.PosOrderLine (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PosOrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL CHECK (UnitPrice >= 0),
    LineTotal AS (Quantity * UnitPrice) PERSISTED,
    CONSTRAINT FK_PosOrderLine_PosOrder FOREIGN KEY (PosOrderId) REFERENCES dbo.PosOrder(Id) ON DELETE CASCADE,
    CONSTRAINT FK_PosOrderLine_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(Id)
);
GO

-- =========================================================================
-- CHỈ MỤC TỐI ƯU HIỆU NĂNG TRUY VẤN (INDEXES)
-- =========================================================================
CREATE NONCLUSTERED INDEX IX_Product_Sku ON dbo.Product(Sku);
CREATE NONCLUSTERED INDEX IX_Product_Category ON dbo.Product(Category);
CREATE NONCLUSTERED INDEX IX_Customer_Phone ON dbo.Customer(Phone);
CREATE NONCLUSTERED INDEX IX_Receipt_Status ON dbo.GoodsReceipt(Status);
CREATE NONCLUSTERED INDEX IX_Receipt_Date ON dbo.GoodsReceipt(ReceiptDate);
CREATE NONCLUSTERED INDEX IX_Issue_Status ON dbo.GoodsIssue(Status);
CREATE NONCLUSTERED INDEX IX_Issue_Date ON dbo.GoodsIssue(IssueDate);
CREATE NONCLUSTERED INDEX IX_PosOrder_Number ON dbo.PosOrder(OrderNumber);
CREATE NONCLUSTERED INDEX IX_PosOrder_Date ON dbo.PosOrder(OrderDate);
CREATE NONCLUSTERED INDEX IX_PosOrder_Status ON dbo.PosOrder(Status);
CREATE NONCLUSTERED INDEX IX_PosOrderLine_Product ON dbo.PosOrderLine(ProductId);
GO

PRINT N'Thiết kế CSDL KhoBanLeDb (9 Bảng chuẩn 3NF tích hợp POS & nối quan hệ khóa ngoại) hoàn tất thành công!';
GO
