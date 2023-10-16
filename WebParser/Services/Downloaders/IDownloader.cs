using HtmlParser;

namespace WebParser.Services.Downloaders
{
    public interface IDownloader
    {
        Task<HtmlDocument> Load(string url);
    }
}
