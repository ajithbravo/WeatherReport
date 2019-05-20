using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using IHttpClientFactory = Services.Proxy.Abstract.IHttpClientFactory;

namespace Services.Proxy
{
    [ExcludeFromCodeCoverage]
    public class HttpClientFactory: IHttpClientFactory
    {
        public HttpClient CreateClient()
        {
            var client = new HttpClient {BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/")};
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
