using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using Services.Proxy;
using Services.ServiceModal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.Tests.Proxy
{
    [TestFixture(Category ="Proxy_Test")]
    public class WeatherProxyTest
    {
        private WeatherProxy _weatherProxy;
        private Services.Proxy.Abstract.IHttpClientFactory _httpClientFactory;
       
       [SetUp]
       public void Init()
        {
            _httpClientFactory = Substitute.For<Services.Proxy.Abstract.IHttpClientFactory>();
            _weatherProxy = new WeatherProxy(_httpClientFactory);
        }

        [Test]
        public void GetWeatherByCity_ShouldReturn_AllWeatherDetails()
        {
            var weatherDetail = new WeatherDetail
            {
                City = new CityDetail { Country = "AU", Name = "Sydney" },
                List = new List<Detail> {
                            new Detail {
                                DateTime = "19/5/2019",
                                Dt_txt = "2019-05-19 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 17.89M,
                                    Temp_Min=17.58M
                                }
                            } }
            };
            var mockHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(weatherDetail), Encoding.UTF8, "application/json")
            });
            var mockHttpClient = new HttpClient(mockHttpMessageHandler);
            mockHttpClient.BaseAddress = new Uri("http://goodurl.com");
            _httpClientFactory.CreateClient().Returns(mockHttpClient);

            var response = _weatherProxy.GetWeatherByCity("2147714");

            Assert.AreEqual(17.58M, response.Result.List[0].Main.Temp_Min);
            Assert.AreEqual(17.89M, response.Result.List[0].Main.Temp_Max);
        }

        public class MockHttpMessageHandler : DelegatingHandler
        {
            private HttpResponseMessage _mockResponse;

            public MockHttpMessageHandler(HttpResponseMessage responseMessage)
            {
                _mockResponse = responseMessage;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_mockResponse);
            }
        }
    }
}
