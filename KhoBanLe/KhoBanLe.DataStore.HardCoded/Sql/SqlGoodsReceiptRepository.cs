using System.Data;
using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    /// <summary>
    /// K2.2: Ghi cặp Header-Line (GoodsReceipt & GoodsReceiptLine) trong 1 Transaction duy nhất
    /// K2.3: 100% truy vấn tham số hóa (Parameterized SQL), tuyệt đối không nối chuỗi
    /// K2.4: Luồng giao dịch chính chạy end-to-end trên SQL Server
    /// </summary>
    public class SqlGoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlGoodsReceiptRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<GoodsReceipt>> GetGoodsReceiptsAsync(ReceiptStatus? status = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT r.Id, r.ReceiptNumber, r.SupplierId, r.ReceiptDate, r.CreatedBy, r.ApprovedBy, r.ApprovedDate, r.Status, r.Note,
                       s.Id, s.SupplierCode, s.Name, s.Phone, s.Email, s.Address, s.IsActive,
                       l.Id, l.GoodsReceiptId, l.ProductId, l.Quantity, l.UnitPrice,
                       p.Id, p.Sku, p.Name, p.Unit, p.Category, p.Price, p.CurrentStock, p.MinStockLevel
                FROM dbo.GoodsReceipt r
                INNER JOIN dbo.Supplier s ON r.SupplierId = s.Id
                LEFT JOIN dbo.GoodsReceiptLine l ON r.Id = l.GoodsReceiptId
                LEFT JOIN dbo.Product p ON l.ProductId = p.Id
                WHERE (@Status IS NULL OR r.Status = @Status)
                ORDER BY r.ReceiptDate DESC, r.Id DESC;";

            var receiptDict = new Dictionary<int, GoodsReceipt>();

            await connection.QueryAsync<GoodsReceipt, Supplier, GoodsReceiptLine, Product, GoodsReceipt>(
                sql,
                (receipt, supplier, line, product) =>
                {
                    if (!receiptDict.TryGetValue(receipt.Id, out var currentReceipt))
                    {
                        currentReceipt = receipt;
                        currentReceipt.Supplier = supplier;
                        currentReceipt.Lines = new List<GoodsReceiptLine>();
                        receiptDict.Add(currentReceipt.Id, currentReceipt);
                    }

                    if (line != null && line.Id > 0)
                    {
                        if (product != null) line.Product = product;
                        currentReceipt.Lines.Add(line);
                    }

                    return currentReceipt;
                },
                new { Status = status.HasValue ? (int?)status.Value : null },
                splitOn: "Id,Id,Id"
            );

            return receiptDict.Values;
        }

        public async Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(int id)
        {
            var list = await GetGoodsReceiptsAsync();
            return list.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// TIÊU CHÍ K2.2: Ghi cặp Header (GoodsReceipt) và Line (GoodsReceiptLine) trong 1 Transaction
        /// </summary>
        public async Task<int> CreateGoodsReceiptAsync(GoodsReceipt receipt)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            // BẮT ĐẦU TRANSACTION ĐẢM BẢO TOÀN VẸN CẶP HEADER-LINE (K2.2)
            using var transaction = connection.BeginTransaction();
            try
            {
                // 1. Tự sinh mã phiếu nếu chưa có
                if (string.IsNullOrWhiteSpace(receipt.ReceiptNumber))
                {
                    receipt.ReceiptNumber = $"PN-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
                }

                receipt.ReceiptDate = DateTime.Now;
                receipt.Status = ReceiptStatus.Draft;

                // 2. Insert Header (GoodsReceipt)
                const string insertHeaderSql = @"
                    INSERT INTO dbo.GoodsReceipt (ReceiptNumber, SupplierId, ReceiptDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
                    VALUES (@ReceiptNumber, @SupplierId, @ReceiptDate, @CreatedBy, @ApprovedBy, @ApprovedDate, @Status, @Note);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int receiptId = await connection.ExecuteScalarAsync<int>(insertHeaderSql, new
                {
                    receipt.ReceiptNumber,
                    receipt.SupplierId,
                    receipt.ReceiptDate,
                    receipt.CreatedBy,
                    receipt.ApprovedBy,
                    receipt.ApprovedDate,
                    Status = (int)receipt.Status,
                    receipt.Note
                }, transaction);

                receipt.Id = receiptId;

                // 3. Insert các dòng LineItems (GoodsReceiptLine) trong cùng 1 Transaction
                const string insertLineSql = @"
                    INSERT INTO dbo.GoodsReceiptLine (GoodsReceiptId, ProductId, Quantity, UnitPrice)
                    VALUES (@GoodsReceiptId, @ProductId, @Quantity, @UnitPrice);";

                foreach (var line in receipt.Lines)
                {
                    line.GoodsReceiptId = receiptId;
                    await connection.ExecuteAsync(insertLineSql, new
                    {
                        GoodsReceiptId = receiptId,
                        line.ProductId,
                        line.Quantity,
                        line.UnitPrice
                    }, transaction);
                }

                // COMMIT TRANSACTION THÀNH CÔNG
                transaction.Commit();
                return receiptId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// TIÊU CHÍ K1.3 & K2.2: Duyệt phiếu nhập & tăng tồn kho trong 1 Transaction an toàn
        /// </summary>
        public async Task ApproveReceiptTransactionAsync(int receiptId, string approvedBy)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                // 1. Cập nhật trạng thái phiếu nhập sang Approved (Status = 1)
                const string updateHeaderSql = @"
                    UPDATE dbo.GoodsReceipt
                    SET Status = 1,
                        ApprovedBy = @ApprovedBy,
                        ApprovedDate = @ApprovedDate
                    WHERE Id = @Id AND Status = 0;";

                int rows = await connection.ExecuteAsync(updateHeaderSql, new
                {
                    Id = receiptId,
                    ApprovedBy = approvedBy,
                    ApprovedDate = DateTime.Now
                }, transaction);

                if (rows > 0)
                {
                    // 2. Lấy danh sách các dòng mặt hàng để tăng tồn kho
                    const string getLinesSql = @"
                        SELECT ProductId, Quantity
                        FROM dbo.GoodsReceiptLine
                        WHERE GoodsReceiptId = @ReceiptId;";

                    var lines = await connection.QueryAsync<(int ProductId, int Quantity)>(getLinesSql, new { ReceiptId = receiptId }, transaction);

                    // 3. Tăng tồn kho tương ứng cho từng mặt hàng trong cùng Transaction
                    const string updateStockSql = @"
                        UPDATE dbo.Product
                        SET CurrentStock = CurrentStock + @Quantity
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

        public async Task CancelReceiptAsync(int receiptId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE dbo.GoodsReceipt
                SET Status = 2 -- Cancelled
                WHERE Id = @Id AND Status = 0;";

            await connection.ExecuteAsync(sql, new { Id = receiptId });
        }
    }
}
