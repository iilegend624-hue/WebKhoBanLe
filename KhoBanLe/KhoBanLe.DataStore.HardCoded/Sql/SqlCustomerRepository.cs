using Dapper;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SqlCustomerRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(string? filter = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT Id, CustomerCode, Name, Phone, Email, Address, Points, CreatedAt
                FROM dbo.Customer
                WHERE (@Filter IS NULL OR Name LIKE @FilterLike OR Phone LIKE @FilterLike OR CustomerCode LIKE @FilterLike)
                ORDER BY Id ASC;";

            var param = new
            {
                Filter = string.IsNullOrWhiteSpace(filter) ? null : filter.Trim(),
                FilterLike = string.IsNullOrWhiteSpace(filter) ? null : $"%{filter.Trim()}%"
            };

            return await connection.QueryAsync<Customer>(sql, param);
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"SELECT * FROM dbo.Customer WHERE Id = @Id;";
            return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
        }

        public async Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"SELECT * FROM dbo.Customer WHERE Phone = @Phone;";
            return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { Phone = phone });
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO dbo.Customer (CustomerCode, Name, Phone, Email, Address, Points)
                VALUES (@CustomerCode, @Name, @Phone, @Email, @Address, @Points);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            int newId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                customer.CustomerCode,
                customer.Name,
                customer.Phone,
                customer.Email,
                customer.Address,
                customer.Points
            });

            customer.Id = newId;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE dbo.Customer
                SET CustomerCode = @CustomerCode,
                    Name = @Name,
                    Phone = @Phone,
                    Email = @Email,
                    Address = @Address,
                    Points = @Points
                WHERE Id = @Id;";

            await connection.ExecuteAsync(sql, new
            {
                customer.CustomerCode,
                customer.Name,
                customer.Phone,
                customer.Email,
                customer.Address,
                customer.Points,
                customer.Id
            });
        }
    }
}
