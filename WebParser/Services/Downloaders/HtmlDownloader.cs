using HtmlParser;
using WebParser.Services.HttpClientServices;

namespace WebParser.Services.Downloaders
{
    public class HtmlDownloader: IDownloader
    {
        private readonly IHttpRequest _httpRequest;

        public HtmlDownloader(IHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public async Task<HtmlDocument> Load(string url)
        {
            var response = await _httpRequest.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to load page {url}");
            
            var html = await response.Content.ReadAsStringAsync();
            return new HtmlDocument(html);
        }
    }
}
