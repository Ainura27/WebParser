using WebParser.Services.Downloaders;
using WebParser.Services.HttpClientServices;
using WebParser.Services.HttpRequestServices;
using WebParser.Services.Parser;
using WebParser.Services;

namespace WebParser.Extensions
{
    public static class StartupExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpRequest, HttpRequestService>();
            services.AddSingleton<IDownloader, HtmlDownloader>();
            services.AddTransient(typeof(IParser<>), typeof(PostParser<>));
            services.AddTransient<PostCrawler>();
        }
    }
}
