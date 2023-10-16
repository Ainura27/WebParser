using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebParser.Data.Entities;
using WebParser.Extensions;
using WebParser.Models.Configurations;

namespace WebParser.Repository
{
    public class PostRepository: IPostRepository
    {
        private readonly DapperContext _context;

        public PostRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(Post post)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("Title", post.Title);
            parameters.Add("Text", post.Text);
            parameters.Add("Date", post.Date);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var id = parameters.Get<int?>("Id");
            await connection.ExecuteAsync("PostInsert", parameters, commandType: CommandType.StoredProcedure);
            return id;
        }

        public async Task<IEnumerable<Post>> GetByDate(DateTime from, DateTime to)
        {
            using var connection = _context.CreateConnection();
            var values = new { from, to };
            return await connection.QueryAsync<Post>("GetPostByDate", values, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<string>> GetTopTen()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<string>("GetTopTen", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Post>> Search(string text)
        {
            using var connection = _context.CreateConnection();
            var values = new { searchText = text };
            return await connection.QueryAsync<Post>("GetPostsByText", values, commandType: CommandType.StoredProcedure);
        }
    }
}
