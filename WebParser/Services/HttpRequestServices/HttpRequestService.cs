using System.Net.Security;
using System.Net;
using System.Net.Http;
using WebParser.Services.HttpClientServices;

namespace WebParser.Services.HttpRequestServices
{
    public class HttpRequestService: IHttpRequest
    {
        private static HttpClient? client;

        public HttpRequestService()
        {
            client = CreateClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url, int timeOut = 180000)
        {
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeOut));
            try
            {
                return await client!.GetAsync(url, cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new Exception($"Timed out {url}");
            }
        }

        private HttpClient CreateClient()
        {
            var handler = GetHttpHandler();
            return new HttpClient(handler);
        }

        private SocketsHttpHandler GetHttpHandler() =>
             new SocketsHttpHandler
            {
                //CookieContainer = CookieContainer,
                UseCookies = true
            };
    }
}
