using WebParser.Data.Entities;

namespace WebParser.Repository
{
    public interface IPostRepository
    {
        Task<int?> Create(Post post);

        Task<IEnumerable<Post>> GetByDate(DateTime from, DateTime to);
        Task<IEnumerable<string>> GetTopTen();
        Task<IEnumerable<Post>> Search(string text);
    }
}
