using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    /// <summary>
    /// K2.3: Truy vấn báo cáo tổng hợp bằng SQL Dapper (Tham số hóa, không nối chuỗi)
    /// K1.3 & Luật 2: Bảo toàn công thức Tồn đầu + Nhập - Xuất = Tồn cuối
    /// </summary>
    public class SqlInventoryReportRepository : IInventoryReportRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlInventoryReportRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<InventoryReportItem>> GetInventoryReportAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT 
                    p.Id AS ProductId,
                    p.Sku,
                    p.Name AS ProductName,
                    p.Unit,
                    p.Category,
                    p.CurrentStock,
                    p.MinStockLevel,
                    ISNULL(r.TotalImport, 0) AS TotalImport,
                    ISNULL(r.TotalImportValue, 0) AS TotalImportValue,
                    ISNULL(i.TotalExport, 0) AS TotalExport,
                    ISNULL(i.TotalExportValue, 0) AS TotalExportValue,
                    (p.CurrentStock - ISNULL(r.TotalImport, 0) + ISNULL(i.TotalExport, 0)) AS BeginningStock,
                    (p.CurrentStock * p.Price) AS EndingStockValue
                FROM dbo.Product p
                LEFT JOIN (
                    SELECT rl.ProductId, 
                           SUM(rl.Quantity) AS TotalImport, 
                           SUM(rl.Quantity * rl.UnitPrice) AS TotalImportValue
                    FROM dbo.GoodsReceiptLine rl
                    INNER JOIN dbo.GoodsReceipt r ON rl.GoodsReceiptId = r.Id
                    WHERE r.Status = 1
                      AND (@FromDate IS NULL OR r.ReceiptDate >= @FromDate)
                      AND (@ToDate IS NULL OR r.ReceiptDate <= @ToDate)
                    GROUP BY rl.ProductId
                ) r ON p.Id = r.ProductId
                LEFT JOIN (
                    SELECT il.ProductId, 
                           SUM(il.Quantity) AS TotalExport, 
                           SUM(il.Quantity * il.UnitPrice) AS TotalExportValue
                    FROM dbo.GoodsIssueLine il
                    INNER JOIN dbo.GoodsIssue i ON il.GoodsIssueId = i.Id
                    WHERE i.Status = 1
                      AND (@FromDate IS NULL OR i.IssueDate >= @FromDate)
                      AND (@ToDate IS NULL OR i.IssueDate <= @ToDate)
                    GROUP BY il.ProductId
                ) i ON p.Id = i.ProductId
                ORDER BY p.Id ASC;";

            var param = new
            {
                FromDate = fromDate,
                ToDate = toDate
            };

            var items = await connection.QueryAsync<InventoryReportItem>(sql, param);
            return items;
        }
    }
}
