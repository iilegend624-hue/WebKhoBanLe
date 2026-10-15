using System.Text;
using KhoBanLe.CoreBusiness;
using Microsoft.JSInterop;

namespace KhoBanLe.Services
{
    public class ExcelExportService
    {
        private readonly IJSRuntime _js;

        public ExcelExportService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task ExportInventoryReportToExcelAsync(IEnumerable<InventoryReportItem> reportItems, string fileName = "BaoCao_NhapXuatTon.csv")
        {
            var sb = new StringBuilder();
            // Thêm UTF-8 BOM để Excel hiển thị tiếng Việt có dấu chuẩn 100%
            sb.Append('\uFEFF');

            // Header
            sb.AppendLine("Mã hàng;Tên sản phẩm;ĐVT;Danh mục;Tồn đầu kỳ;Tổng nhập;Tổng xuất;Tồn cuối kỳ;Tồn thực tế;Khớp đối soát;Định mức tối thiểu;Đơn giá (VNĐ);Giá trị tồn (VNĐ)");

            // Rows
            foreach (var item in reportItems)
            {
                var balancedText = item.IsBalanced ? "KHỚP 100%" : "LỆCH";
                sb.AppendLine($"\"{item.Sku}\";\"{item.ProductName}\";\"{item.Unit}\";\"{item.Category}\";{item.BeginningStock};{item.TotalImport};{item.TotalExport};{item.EndingStock};{item.CurrentStock};\"{balancedText}\";{item.MinStockLevel};{item.EndingStockValue / (item.EndingStock == 0 ? 1 : item.EndingStock):N0};{item.EndingStockValue:N0}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            await DownloadFileAsync(fileName, bytes, "text/csv;charset=utf-8;");
        }

        public async Task ExportProductListToExcelAsync(IEnumerable<Product> products, string fileName = "DanhSach_HangHoa.csv")
        {
            var sb = new StringBuilder();
            sb.Append('\uFEFF');

            sb.AppendLine("ID;Mã SKU;Tên hàng hóa;Đơn vị tính;Danh mục;Đơn giá bán;Tồn kho hiện tại;Định mức tồn tối thiểu;Trạng thái");

            foreach (var p in products)
            {
                string status = p.CurrentStock <= 0 ? "Hết hàng" : (p.CurrentStock <= p.MinStockLevel ? "Sắp hết" : "Còn hàng");
                sb.AppendLine($"{p.Id};\"{p.Sku}\";\"{p.Name}\";\"{p.Unit}\";\"{p.Category}\";{p.Price:N0};{p.CurrentStock};{p.MinStockLevel};\"{status}\"");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            await DownloadFileAsync(fileName, bytes, "text/csv;charset=utf-8;");
        }

        public async Task ExportPosOrdersToExcelAsync(IEnumerable<PosOrder> orders, string fileName = "DanhSach_HoaDon_POS.csv")
        {
            var sb = new StringBuilder();
            sb.Append('\uFEFF');

            sb.AppendLine("Mã đơn POS;Mã phiếu xuất kho;Ngày giờ;Khách hàng;Thu ngân;Phương thức TT;Tổng số lượng;Tổng tiền (VNĐ);Tiền khách đưa;Tiền thối;Trạng thái");

            foreach (var o in orders)
            {
                string issueRef = o.GoodsIssue != null ? o.GoodsIssue.IssueNumber : (o.GoodsIssueId.HasValue ? $"PX-ID#{o.GoodsIssueId}" : "N/A");
                sb.AppendLine($"\"{o.OrderNumber}\";\"{issueRef}\";\"{o.OrderDate:dd/MM/yyyy HH:mm}\";\"{o.CustomerName}\";\"{o.CashierName}\";\"{o.PaymentMethod}\";{o.TotalItems};{o.TotalAmount:N0};{o.ReceivedAmount:N0};{o.ChangeAmount:N0};\"{o.Status}\"");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            await DownloadFileAsync(fileName, bytes, "text/csv;charset=utf-8;");
        }

        private async Task DownloadFileAsync(string fileName, byte[] content, string contentType)
        {
            var base64 = Convert.ToBase64String(content);
            await _js.InvokeVoidAsync("downloadFileFromBase64", fileName, base64, contentType);
        }
    }
}
