using System.Net.Http;

namespace Services.Proxy.Abstract
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}
