using HtmlParser;
using WebParser.Data.Entities;
using WebParser.Repository;
using WebParser.Services.Downloaders;
using WebParser.Services.Parser;

namespace WebParser.Services
{
    public class PostCrawler
    {
        private readonly IDownloader _downloader;
        private readonly IPostRepository _postRepository;
        private readonly IParser<Post> _parser;

        public PostCrawler(IDownloader downloader,
                       IParser<Post> parser,
                       IPostRepository postRepository)
        {
            _downloader = downloader;
            _parser = parser;
            _postRepository = postRepository;
        }

        public async Task Crawle(params string[] urls)
        {
            foreach (var url in urls)
            {
                var htmlDocument = await _downloader.Load(url);
                var entity = _parser.ParseAsync(htmlDocument);
                await _postRepository.Create(entity);
            }
        }
    }
}
