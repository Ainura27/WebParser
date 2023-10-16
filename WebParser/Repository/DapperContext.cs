using Microsoft.Data.SqlClient;
using System.Data;
using WebParser.Models.Configurations;

namespace WebParser.Repository
{
    public class DapperContext
    {
        private readonly string _connection;
        private readonly string _masterConnection;
        public DapperContext(MainConfiguration configuration)
        {
            _connection = configuration.Get().Mssql.ConnectionString;
            _masterConnection = configuration.Get().Master.ConnectionString;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connection);

        public IDbConnection CreateMasterConnection()
            => new SqlConnection(_masterConnection);
    }
}
