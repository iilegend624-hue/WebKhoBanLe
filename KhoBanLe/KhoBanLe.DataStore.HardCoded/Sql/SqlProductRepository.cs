using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    /// <summary>
    /// K2.3 & K2.4: SQL Repository Sản Phẩm sử dụng Dapper & truy vấn tham số hóa 100%
    /// </summary>
    public class SqlProductRepository : IProductRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlProductRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string? filter = null, string? category = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT Id, Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt
                FROM dbo.Product
                WHERE (@Filter IS NULL OR Name LIKE @FilterLike OR Sku LIKE @FilterLike)
                  AND (@Category IS NULL OR Category = @Category)
                ORDER BY Id ASC;";

            var param = new
            {
                Filter = string.IsNullOrWhiteSpace(filter) ? null : filter.Trim(),
                FilterLike = string.IsNullOrWhiteSpace(filter) ? null : $"%{filter.Trim()}%",
                Category = string.IsNullOrWhiteSpace(category) ? null : category.Trim()
            };

            return await connection.QueryAsync<Product>(sql, param);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT Id, Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt
                FROM dbo.Product
                WHERE Id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<Product?> GetProductBySkuAsync(string sku)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT Id, Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl, CreatedAt
                FROM dbo.Product
                WHERE Sku = @Sku;";

            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Sku = sku });
        }

        public async Task AddProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
                VALUES (@Sku, @Name, @Unit, @Category, @Price, @CurrentStock, @MinStockLevel, @Description, @ImageUrl);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            int newId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                product.Sku,
                product.Name,
                product.Unit,
                product.Category,
                product.Price,
                product.CurrentStock,
                product.MinStockLevel,
                product.Description,
                product.ImageUrl
            });

            product.Id = newId;
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE dbo.Product
                SET Sku = @Sku,
                    Name = @Name,
                    Unit = @Unit,
                    Category = @Category,
                    Price = @Price,
                    MinStockLevel = @MinStockLevel,
                    Description = @Description,
                    ImageUrl = @ImageUrl
                WHERE Id = @Id;";

            await connection.ExecuteAsync(sql, new
            {
                product.Sku,
                product.Name,
                product.Unit,
                product.Category,
                product.Price,
                product.MinStockLevel,
                product.Description,
                product.ImageUrl,
                product.Id
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"DELETE FROM dbo.Product WHERE Id = @Id;";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdateStockAsync(int productId, int quantityChange)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE dbo.Product
                SET CurrentStock = CurrentStock + @QuantityChange
                WHERE Id = @ProductId;";

            await connection.ExecuteAsync(sql, new
            {
                ProductId = productId,
                QuantityChange = quantityChange
            });
        }
    }
}
