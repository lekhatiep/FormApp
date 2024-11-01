using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DataContext
{
    public class DapperDbConnectionFactory : IDapperDbConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperDbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
