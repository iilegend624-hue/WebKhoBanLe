using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Reports
{
    public class InventoryAuditReport
    {
        public List<InventoryReportItem> Items { get; set; } = new();
        public bool IsAuditPassed { get; set; } = true;
        public int DiscrepancyCount { get; set; } = 0;
        public string AuditSummaryMessage { get; set; } = string.Empty;

        public int TotalBeginningStock => Items?.Sum(i => i.BeginningStock) ?? 0;
        public int TotalImportQuantity => Items?.Sum(i => i.TotalImport) ?? 0;
        public int TotalExportQuantity => Items?.Sum(i => i.TotalExport) ?? 0;
        public int TotalEndingStock => Items?.Sum(i => i.EndingStock) ?? 0;
        public double TotalStockValue => Items?.Sum(i => i.EndingStockValue) ?? 0;
    }

    public interface IGetInventoryReportUseCase
    {
        Task<InventoryAuditReport> ExecuteAsync(DateTime? fromDate = null, DateTime? toDate = null);
    }

    /// <summary>
    /// LUẬT 2: BẢO TOÀN CÔNG THỨC NHẬP - XUẤT - TỒN
    /// Vị trí: KhoBanLe.UseCases.Reports.GetInventoryReportUseCase
    /// - Công thức bất biến: Tồn đầu kỳ + Tổng nhập (Approved) - Tổng xuất (Approved) = Tồn cuối kỳ.
    /// - Khóa phiếu đã duyệt: Không cho phép sửa đổi hoặc xóa phiếu khi đã ở trạng thái Approved.
    /// - Hàm đối soát kiểm tra chéo: Phát hiện sai lệch số liệu tồn kho thực tế so với chứng từ nhập/xuất.
    /// </summary>
    public class GetInventoryReportUseCase : IGetInventoryReportUseCase
    {
        private readonly IInventoryReportRepository _reportRepository;

        public GetInventoryReportUseCase(IInventoryReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<InventoryAuditReport> ExecuteAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var items = (await _reportRepository.GetInventoryReportAsync(fromDate, toDate)).ToList();

            var discrepancies = items.Where(i => !i.IsBalanced).ToList();
            bool passed = !discrepancies.Any();

            string summaryMessage = passed
                ? "Đối soát thành công: 100% mặt hàng bảo toàn chuẩn công thức Nhập - Xuất - Tồn và khớp hoàn toàn với sổ cái kho."
                : $"CẢNH BÁO ĐỐI SOÁT: Phát hiện {discrepancies.Count} mặt hàng có chênh lệch giữa chứng từ giao dịch và tồn kho thực tế!";

            return new InventoryAuditReport
            {
                Items = items,
                IsAuditPassed = passed,
                DiscrepancyCount = discrepancies.Count,
                AuditSummaryMessage = summaryMessage
            };
        }
    }
}
