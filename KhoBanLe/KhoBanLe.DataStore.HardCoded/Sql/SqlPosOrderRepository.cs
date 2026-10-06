using System.Data;
using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    /// <summary>
    /// K2.2: Ghi Hóa đơn bán lẻ POS và dòng hóa đơn trong 1 Transaction, đồng bộ trừ kho và tạo phiếu xuất
    /// K2.3: Parameterized SQL toàn bộ
    /// </summary>
    public class SqlPosOrderRepository : IPosOrderRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlPosOrderRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<PosOrder>> GetPosOrdersAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT o.Id, o.OrderNumber, o.CustomerId, o.CustomerName, o.OrderDate, o.CashierName, o.PaymentMethod,
                       o.SubTotal, o.DiscountAmount, o.TotalAmount, o.ReceivedAmount, o.ChangeAmount, o.GoodsIssueId, o.Status, o.Note,
                       c.Id, c.CustomerCode, c.Name, c.Phone, c.Email, c.Address, c.Points,
                       l.Id, l.PosOrderId, l.ProductId, l.Quantity, l.UnitPrice,
                       p.Id, p.Sku, p.Name, p.Unit, p.Category, p.Price, p.CurrentStock, p.MinStockLevel
                FROM dbo.PosOrder o
                LEFT JOIN dbo.Customer c ON o.CustomerId = c.Id
                LEFT JOIN dbo.PosOrderLine l ON o.Id = l.PosOrderId
                LEFT JOIN dbo.Product p ON l.ProductId = p.Id
                WHERE (@FromDate IS NULL OR o.OrderDate >= @FromDate)
                  AND (@ToDate IS NULL OR o.OrderDate <= @ToDate)
                ORDER BY o.OrderDate DESC, o.Id DESC;";

            var orderDict = new Dictionary<int, PosOrder>();

            await connection.QueryAsync<PosOrder, Customer, PosOrderLine, Product, PosOrder>(
                sql,
                (order, customer, line, product) =>
                {
                    if (!orderDict.TryGetValue(order.Id, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.Customer = customer;
                        currentOrder.Lines = new List<PosOrderLine>();
                        orderDict.Add(currentOrder.Id, currentOrder);
                    }

                    if (line != null && line.Id > 0)
                    {
                        if (product != null) line.Product = product;
                        currentOrder.Lines.Add(line);
                    }

                    return currentOrder;
                },
                new { FromDate = fromDate, ToDate = toDate },
                splitOn: "Id,Id,Id"
            );

            return orderDict.Values;
        }

        public async Task<PosOrder?> GetPosOrderByIdAsync(int id)
        {
            var list = await GetPosOrdersAsync();
            return list.FirstOrDefault(o => o.Id == id);
        }

        public async Task<PosOrder?> GetPosOrderByNumberAsync(string orderNumber)
        {
            var list = await GetPosOrdersAsync();
            return list.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        public async Task<int> CreatePosOrderAsync(PosOrder order)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                if (string.IsNullOrWhiteSpace(order.OrderNumber))
                {
                    order.OrderNumber = $"POS-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
                }

                order.OrderDate = DateTime.Now;
                order.Status = "Paid";

                // 1. Tự động tạo Phiếu xuất kho bán lẻ (GoodsIssue) trong cùng Transaction để trừ kho
                string issueNumber = $"PX-POS-{DateTime.Now:yyyyMMdd-HHmmss}";
                const string insertIssueSql = @"
                    INSERT INTO dbo.GoodsIssue (IssueNumber, CustomerOrDepartment, IssueDate, CreatedBy, ApprovedBy, ApprovedDate, Status, Note)
                    VALUES (@IssueNumber, @CustomerOrDepartment, @IssueDate, @CreatedBy, @ApprovedBy, @ApprovedDate, 1, @Note);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int issueId = await connection.ExecuteScalarAsync<int>(insertIssueSql, new
                {
                    IssueNumber = issueNumber,
                    CustomerOrDepartment = order.CustomerName,
                    IssueDate = order.OrderDate,
                    CreatedBy = order.CashierName,
                    ApprovedBy = "Hệ thống POS",
                    ApprovedDate = order.OrderDate,
                    Note = $"Tự động xuất kho từ đơn bán lẻ {order.OrderNumber}"
                }, transaction);

                order.GoodsIssueId = issueId;

                // 2. Insert PosOrder Header
                const string insertOrderSql = @"
                    INSERT INTO dbo.PosOrder (OrderNumber, CustomerId, CustomerName, OrderDate, CashierName, PaymentMethod, SubTotal, DiscountAmount, TotalAmount, ReceivedAmount, ChangeAmount, GoodsIssueId, Status, Note)
                    VALUES (@OrderNumber, @CustomerId, @CustomerName, @OrderDate, @CashierName, @PaymentMethod, @SubTotal, @DiscountAmount, @TotalAmount, @ReceivedAmount, @ChangeAmount, @GoodsIssueId, @Status, @Note);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int orderId = await connection.ExecuteScalarAsync<int>(insertOrderSql, new
                {
                    order.OrderNumber,
                    order.CustomerId,
                    order.CustomerName,
                    order.OrderDate,
                    order.CashierName,
                    order.PaymentMethod,
                    order.SubTotal,
                    order.DiscountAmount,
                    order.TotalAmount,
                    order.ReceivedAmount,
                    order.ChangeAmount,
                    GoodsIssueId = issueId,
                    order.Status,
                    order.Note
                }, transaction);

                order.Id = orderId;

                // Cập nhật ngược PosOrderId vào GoodsIssue
                await connection.ExecuteAsync("UPDATE dbo.GoodsIssue SET PosOrderId = @PosOrderId WHERE Id = @IssueId;",
                    new { PosOrderId = orderId, IssueId = issueId }, transaction);

                // 3. Insert PosOrderLine & GoodsIssueLine & Giảm tồn kho trong cùng Transaction
                const string insertOrderLineSql = @"
                    INSERT INTO dbo.PosOrderLine (PosOrderId, ProductId, Quantity, UnitPrice)
                    VALUES (@PosOrderId, @ProductId, @Quantity, @UnitPrice);";

                const string insertIssueLineSql = @"
                    INSERT INTO dbo.GoodsIssueLine (GoodsIssueId, ProductId, Quantity, UnitPrice)
                    VALUES (@GoodsIssueId, @ProductId, @Quantity, @UnitPrice);";

                const string updateStockSql = @"
                    UPDATE dbo.Product
                    SET CurrentStock = CurrentStock - @Quantity
                    WHERE Id = @ProductId;";

                foreach (var line in order.Lines)
                {
                    line.PosOrderId = orderId;
                    await connection.ExecuteAsync(insertOrderLineSql, new
                    {
                        PosOrderId = orderId,
                        line.ProductId,
                        line.Quantity,
                        line.UnitPrice
                    }, transaction);

                    await connection.ExecuteAsync(insertIssueLineSql, new
                    {
                        GoodsIssueId = issueId,
                        line.ProductId,
                        line.Quantity,
                        line.UnitPrice
                    }, transaction);

                    await connection.ExecuteAsync(updateStockSql, new
                    {
                        line.Quantity,
                        line.ProductId
                    }, transaction);
                }

                transaction.Commit();
                return orderId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
