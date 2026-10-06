using System.Data;
using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    /// <summary>
    /// K2.2: Ghi cặp Header-Line (GoodsIssue & GoodsIssueLine) trong 1 Transaction duy nhất
    /// K2.3: 100% truy vấn tham số hóa (Parameterized SQL), không nối chuỗi
    /// K2.4: Luồng giao dịch xuất kho chạy end-to-end trên SQL Server
    /// </summary>
    public class SqlGoodsIssueRepository : IGoodsIssueRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlGoodsIssueRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<GoodsIssue>> GetGoodsIssuesAsync(ReceiptStatus? status = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT i.Id, i.IssueNumber, i.CustomerOrDepartment, i.IssueDate, i.CreatedBy, i.ApprovedBy, i.ApprovedDate, i.Status, i.Note, i.PosOrderId,
                       l.Id, l.GoodsIssueId, l.ProductId, l.Quantity, l.UnitPrice,
                       p.Id, p.Sku, p.Name, p.Unit, p.Category, p.Price, p.CurrentStock, p.MinStockLevel
                FROM dbo.GoodsIssue i
                LEFT JOIN dbo.GoodsIssueLine l ON i.Id = l.GoodsIssueId
                LEFT JOIN dbo.Product p ON l.ProductId = p.Id
                WHERE (@Status IS NULL OR i.Status = @Status)
                ORDER BY i.IssueDate DESC, i.Id DESC;";

            var issueDict = new Dictionary<int, GoodsIssue>();

            await connection.QueryAsync<GoodsIssue, GoodsIssueLine, Product, GoodsIssue>(
                sql,
                (issue, line, product) =>
                {
                    if (!issueDict.TryGetValue(issue.Id, out var currentIssue))
                    {
                        currentIssue = issue;
                        currentIssue.Lines = new List<GoodsIssueLine>();
                        issueDict.Add(currentIssue.Id, currentIssue);
                    }

                    if (line != null && line.Id > 0)
                    {
                        if (product != null) line.Product = product;
                        currentIssue.Lines.Add(line);
                    }

                    return currentIssue;
                },
                new { Status = status.HasValue ? (int?)status.Value : null },
                splitOn: "Id,Id"
            );

            return issueDict.Values;
        }

        public async Task<GoodsIssue?> GetGoodsIssueByIdAsync(int id)
        {
            var list = await GetGoodsIssuesAsync();
            return list.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// TIÊU CHÍ K2.2: Ghi cặp Header (GoodsIssue) và Line (GoodsIssueLine) trong 1 Transaction
        /// </summary>
        public async Task<int> CreateGoodsIssueAsync(GoodsIssue issue)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                if (string.IsNullOrWhiteSpace(issue.IssueNumber))
                {
                    issue.IssueNumber = $"PX-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
                }

                issue.IssueDate = DateTime.Now;
                issue.Status = ReceiptStatus.Draft;

                // 1. Insert Header
                const string insertHeaderSql = @"
                    INSERT INTO dbo.GoodsIssue (IssueNumber, CustomerOrDepartment, IssueDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note, PosOrderId)
                    VALUES (@IssueNumber, @CustomerOrDepartment, @IssueDate, @CreatedBy, @ApprovedBy, @ApprovedDate, @Status, @Note, @PosOrderId);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int issueId = await connection.ExecuteScalarAsync<int>(insertHeaderSql, new
                {
                    issue.IssueNumber,
                    issue.CustomerOrDepartment,
                    issue.IssueDate,
                    issue.CreatedBy,
                    issue.ApprovedBy,
                    issue.ApprovedDate,
                    Status = (int)issue.Status,
                    issue.Note,
                    issue.PosOrderId
                }, transaction);

                issue.Id = issueId;

                // 2. Insert Lines trong cùng 1 Transaction (K2.2)
                const string insertLineSql = @"
                    INSERT INTO dbo.GoodsIssueLine (GoodsIssueId, ProductId, Quantity, UnitPrice)
                    VALUES (@GoodsIssueId, @ProductId, @Quantity, @UnitPrice);";

                foreach (var line in issue.Lines)
                {
                    line.GoodsIssueId = issueId;
                    await connection.ExecuteAsync(insertLineSql, new
                    {
                        GoodsIssueId = issueId,
                        line.ProductId,
                        line.Quantity,
                        line.UnitPrice
                    }, transaction);
                }

                transaction.Commit();
                return issueId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// TIÊU CHÍ K1.3 & K2.2: Duyệt phiếu xuất & trừ tồn kho trong 1 Transaction an toàn
        /// </summary>
        public async Task ApproveIssueTransactionAsync(int issueId, string approvedBy)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                // 1. Cập nhật trạng thái phiếu xuất sang Approved (Status = 1)
                const string updateHeaderSql = @"
                    UPDATE dbo.GoodsIssue
                    SET Status = 1,
                        ApprovedBy = @ApprovedBy,
                        ApprovedDate = @ApprovedDate
                    WHERE Id = @Id AND Status = 0;";

                int rows = await connection.ExecuteAsync(updateHeaderSql, new
                {
                    Id = issueId,
                    ApprovedBy = approvedBy,
                    ApprovedDate = DateTime.Now
                }, transaction);

                if (rows > 0)
                {
                    // 2. Lấy các dòng mặt hàng cần trừ tồn
                    const string getLinesSql = @"
                        SELECT ProductId, Quantity
                        FROM dbo.GoodsIssueLine
                        WHERE GoodsIssueId = @IssueId;";

                    var lines = await connection.QueryAsync<(int ProductId, int Quantity)>(getLinesSql, new { IssueId = issueId }, transaction);

                    // 3. Trừ tồn kho trong cùng Transaction
                    const string updateStockSql = @"
                        UPDATE dbo.Product
                        SET CurrentStock = CurrentStock - @Quantity
                        WHERE Id = @ProductId;";

                    foreach (var line in lines)
                    {
                        await connection.ExecuteAsync(updateStockSql, new
                        {
                            line.Quantity,
                            line.ProductId
                        }, transaction);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task CancelIssueAsync(int issueId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE dbo.GoodsIssue
                SET Status = 2 -- Cancelled
                WHERE Id = @Id AND Status = 0;";

            await connection.ExecuteAsync(sql, new { Id = issueId });
        }
    }
}
