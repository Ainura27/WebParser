using Dapper;
using System.Xml.Linq;

namespace WebParser.Repository
{
    public class InitializeDatabase
    {
        private readonly DapperContext _context;
        public InitializeDatabase(DapperContext context)
        {
            _context = context;
        }

        public void CreateDatabase(string dbName)
        {
            using var connection = _context.CreateMasterConnection();
            var parameters = new { name = dbName };
            var records = connection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);
            if (!records.Any())
                connection.Execute($"create database {dbName}");
        }

        public void CreateTable(string tableName, string sqlScript)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { name = tableName };
            var records = connection.Query("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @name", parameters);
            if (!records.Any())
                connection.Execute(sqlScript);
        }

        public void CreateProcedure(string procName, string sqlScript)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { proc = procName };
            var records = connection.Query(@$"SELECT * FROM sys.objects WHERE name = @proc AND type IN ('P', 'PC' )", parameters);
            if (!records.Any())
                connection.Execute(sqlScript);
        }
    }
}
