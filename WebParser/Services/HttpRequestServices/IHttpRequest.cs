namespace WebParser.Services.HttpClientServices
{
    public interface IHttpRequest
    {
        Task<HttpResponseMessage> GetAsync(string url, int timeOut = 180000);
    }
}
