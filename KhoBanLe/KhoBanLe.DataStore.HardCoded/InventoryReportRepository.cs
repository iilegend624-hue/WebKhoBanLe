using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class InventoryReportRepository : IInventoryReportRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IGoodsReceiptRepository _receiptRepository;
        private readonly IGoodsIssueRepository _issueRepository;

        public InventoryReportRepository(
            IProductRepository productRepository,
            IGoodsReceiptRepository receiptRepository,
            IGoodsIssueRepository issueRepository)
        {
            _productRepository = productRepository;
            _receiptRepository = receiptRepository;
            _issueRepository = issueRepository;
        }

        public async Task<IEnumerable<InventoryReportItem>> GetInventoryReportAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var products = (await _productRepository.GetProductsAsync()).ToList();
            var approvedReceipts = (await _receiptRepository.GetGoodsReceiptsAsync(ReceiptStatus.Approved)).ToList();
            var approvedIssues = (await _issueRepository.GetGoodsIssuesAsync(ReceiptStatus.Approved)).ToList();

            if (fromDate.HasValue)
            {
                approvedReceipts = approvedReceipts.Where(r => r.ReceiptDate >= fromDate.Value).ToList();
                approvedIssues = approvedIssues.Where(i => i.IssueDate >= fromDate.Value).ToList();
            }

            if (toDate.HasValue)
            {
                approvedReceipts = approvedReceipts.Where(r => r.ReceiptDate <= toDate.Value).ToList();
                approvedIssues = approvedIssues.Where(i => i.IssueDate <= toDate.Value).ToList();
            }

            var reportList = new List<InventoryReportItem>();

            foreach (var product in products)
            {
                // Tổng nhập đã duyệt trong kỳ
                var receiptLines = approvedReceipts
                    .SelectMany(r => r.Lines)
                    .Where(l => l.ProductId == product.Id)
                    .ToList();
                int totalImport = receiptLines.Sum(l => l.Quantity);
                double totalImportValue = receiptLines.Sum(l => l.LineTotal);

                // Tổng xuất đã duyệt trong kỳ
                var issueLines = approvedIssues
                    .SelectMany(i => i.Lines)
                    .Where(l => l.ProductId == product.Id)
                    .ToList();
                int totalExport = issueLines.Sum(l => l.Quantity);
                double totalExportValue = issueLines.Sum(l => l.LineTotal);

                // Tồn đầu kỳ suy ra từ chứng từ thực tế: Tồn đầu = Tồn hiện tại - Nhập + Xuất
                int beginningStock = product.CurrentStock - totalImport + totalExport;
                if (beginningStock < 0) beginningStock = 0;

                var item = new InventoryReportItem
                {
                    ProductId = product.Id,
                    Sku = product.Sku,
                    ProductName = product.Name,
                    Unit = product.Unit,
                    Category = product.Category,
                    BeginningStock = beginningStock,
                    TotalImport = totalImport,
                    TotalExport = totalExport,
                    CurrentStock = product.CurrentStock,
                    MinStockLevel = product.MinStockLevel,
                    TotalImportValue = totalImportValue,
                    TotalExportValue = totalExportValue,
                    EndingStockValue = product.CurrentStock * product.Price
                };

                reportList.Add(item);
            }

            return reportList;
        }
    }
}
