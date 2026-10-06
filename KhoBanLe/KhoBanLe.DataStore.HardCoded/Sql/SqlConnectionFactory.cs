using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KhoBanLe.DataStore.HardCoded.Sql
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
        string ConnectionString { get; }
    }

    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=.;Database=KhoBanLe;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public string ConnectionString => _connectionString;

        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
