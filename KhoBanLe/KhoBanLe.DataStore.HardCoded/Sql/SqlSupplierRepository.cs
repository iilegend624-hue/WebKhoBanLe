using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    public class SqlSupplierRepository : ISupplierRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlSupplierRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT Id, SupplierCode, Name, Phone, Email, Address, IsActive, CreatedAt
                FROM dbo.Supplier
                WHERE IsActive = 1
                ORDER BY Name ASC;";

            return await connection.QueryAsync<Supplier>(sql);
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT Id, SupplierCode, Name, Phone, Email, Address, IsActive, CreatedAt
                FROM dbo.Supplier
                WHERE Id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<Supplier>(sql, new { Id = id });
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Supplier (SupplierCode, Name, Phone, Email, Address, IsActive)
                VALUES (@SupplierCode, @Name, @Phone, @Email, @Address, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            int newId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                supplier.SupplierCode,
                supplier.Name,
                supplier.Phone,
                supplier.Email,
                supplier.Address,
                supplier.IsActive
            });

            supplier.Id = newId;
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE dbo.Supplier
                SET SupplierCode = @SupplierCode,
                    Name = @Name,
                    Phone = @Phone,
                    Email = @Email,
                    Address = @Address,
                    IsActive = @IsActive
                WHERE Id = @Id;";

            await connection.ExecuteAsync(sql, new
            {
                supplier.SupplierCode,
                supplier.Name,
                supplier.Phone,
                supplier.Email,
                supplier.Address,
                supplier.IsActive,
                supplier.Id
            });
        }
    }
}
