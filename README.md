# ĐỒ ÁN CAPSTONE (ECO2415) — ĐỀ TÀI 13: KHO BÁN LẺ (NHẬP – XUẤT – TỒN)
> **Hệ thống Quản lý Kho Bán lẻ & Bán hàng tại quầy (POS) chuẩn Clean Architecture 4 tầng (.NET 8 Blazor Interactive Server & SQL Server SSMS)**  
> Căn cứ theo văn bản: **18 Đề tài Đồ án Capstone — ECO2415 (Bộ môn TMĐT)**

---

## 1. Giới Thiệu & Kiến Trúc 4 Tầng

Hệ thống được thiết kế theo đúng nguyên tắc **"eShop đổi miền nghiệp vụ"** qua 35 video của học phần ECO2415:
- **`KhoBanLe.CoreBusiness`**: Tầng lõi trong cùng, chứa các thực thể (`Product`, `Supplier`, `Customer`, `GoodsReceipt`, `GoodsReceiptLine`, `GoodsIssue`, `GoodsIssueLine`, `PosOrder`, `PosOrderLine`, `InventoryReportItem`, `ReceiptStatus`). Không phụ thuộc bất kỳ project hay thư viện bên ngoài nào.
- **`KhoBanLe.UseCases`**: Tầng nghiệp vụ chứa logic ứng dụng, định nghĩa các Interface Repository (`IProductRepository`, `IGoodsReceiptRepository`, `IGoodsIssueRepository`, `IInventoryReportRepository`, `ICustomerRepository`, `IPosOrderRepository`) và cài đặt 2 luật nghiệp vụ bắt buộc (`ApproveGoodsIssueUseCase`, `GetInventoryReportUseCase`). Chỉ tham chiếu `CoreBusiness`.
- **`KhoBanLe.DataStore.HardCoded` (Plugins / Infrastructure)**: Cài đặt các Interface Repository từ UseCases. Cung cấp 2 Plugin:
  1. **SQL Server Plugin (`Sql/`)**: Kết nối SQL Server thông qua Dapper và `Microsoft.Data.SqlClient`, đảm bảo 100% truy vấn tham số hóa (K2.3) và thực thi ghi Header-Line trong 1 Database Transaction (K2.2).
  2. **In-Memory Plugin**: Phục vụ kiểm thử offline và minh chứng nguyên tắc Dependency Rule khi vấn đáp "Đổi Plugin Dữ Liệu".
- **`KhoBanLe` (WebApp - Blazor Interactive Server)**: Composition Root của toàn bộ ứng dụng, đăng ký Dependency Injection tại `Program.cs`, bảo vệ phân quyền 2 vai trò (K4.1, K4.2) và giao diện người dùng tương thích đa thiết bị (K3.4).

---

## 2. Hai Luật Nghiệp Vụ Cốt Lõi

1. **LUẬT 1: Duyệt mới đổi tồn & Chặn xuất âm kho (K1.3, K3.3)**
   - *Vị trí cài đặt:* `KhoBanLe.UseCases/GoodsIssues/ApproveGoodsIssueUseCase.cs` (Dòng 72 - 90).
   - *Nguyên tắc:* Tồn kho hàng hóa CHỈ thay đổi khi phiếu xuất được Quản lý kho phê duyệt trong 1 Database Transaction. Phiếu xuất có mặt hàng vượt tồn kho hiện tại BẮT BUỘC BỊ CHẶN và trả về thông báo lỗi thân thiện (không văng exception).
   - *Ca demo sẵn có:* Phiếu **PX-2026-002** yêu cầu xuất 10 chiếc *Bút bi Thiên Long TL-027* (trong khi tồn kho thực tế chỉ có 5 chiếc). Bấm duyệt sẽ bị chặn an toàn.

2. **LUẬT 2: Bảo toàn công thức Nhập - Xuất - Tồn & Đối soát chéo (K1.3, K4.3)**
   - *Vị trí cài đặt:* `KhoBanLe.UseCases/Reports/GetInventoryReportUseCase.cs` (Dòng 42 - 59) & `KhoBanLe.CoreBusiness/InventoryReportItem.cs` (Dòng 19 - 26).
   - *Nguyên tắc:* Công thức bất biến: `Tồn cuối kỳ = Tồn đầu kỳ + Tổng nhập (Approved) - Tổng xuất (Approved)`. Chứng từ đã duyệt bị khóa, không được chỉnh sửa/xóa. Hệ thống có tính năng kiểm tra đối soát chéo độc lập với tồn kho thực tế và xuất dữ liệu thật ra Excel/CSV UTF-8 BOM.

---

## 3. CSDL SQL Server & 3 Script SQL Chuẩn 3NF (K2.1)

Thư mục `KhoBanLe/SqlScripts` cung cấp 3 tệp SQL thực thi trực tiếp trên SQL Server Management Studio (SSMS):
1. **`01_schema.sql`**: Tạo Database `KhoBanLe`, 9 bảng chuẩn 3NF với 10 liên kết khóa ngoại (Foreign Keys), ràng buộc CHECK giá trị không âm và Indexes tối ưu truy vấn.
2. **`02_seed_data.sql`**: Dữ liệu mẫu chuẩn logic khớp 100% với Codebase C# (4 Nhà cung cấp, 8 Sản phẩm, 3 Khách hàng, 3 Phiếu nhập, 3 Phiếu xuất và Hóa đơn POS).
3. **`03_pos_queries_and_diagram.sql`**: View chi tiết đơn POS (`v_PosOrderDetails`), View doanh thu theo ngày (`v_DailyPosRevenue`), Stored Procedure bán hàng trong 1 Transaction (`sp_ProcessPosCheckout`) và truy vấn kiểm chứng công thức Luật 2.

---

## 4. Hướng Dẫn Chạy Dự Án

### Yêu Cầu Môi Trường
- .NET 8.0 SDK (hoặc .NET 10.0 LTS)
- SQL Server (Local, Express hoặc Developer Edition)

### Các Bước Chạy Ứng Dụng:
```bash
# 1. Khởi tạo CSDL (nếu chưa có):
# Mở SSMS và lần lượt thực thi 3 script trong thư mục KhoBanLe/SqlScripts/

# 2. Chuyển vào thư mục WebApp:
cd KhoBanLe/KhoBanLe

# 3. Chạy ứng dụng:
dotnet run
```
Mở trình duyệt truy cập: `http://localhost:5000` (hoặc cổng HTTPS hiển thị trên terminal).

---

## 5. Bằng Chứng Phiếu Chấm Chi Tiết (GRADING.md)

Xem chi tiết 17 mã kiểm tra (K1.1 &rarr; K5.3) tại tệp [GRADING.md](file:///c:/Users/trung/OneDrive/Máy%20tính/WebKhoBanLe/GRADING.md) ở gốc repository hoặc truy cập trang `/guide` ngay trên ứng dụng web để tra cứu tương tác trực quan.
