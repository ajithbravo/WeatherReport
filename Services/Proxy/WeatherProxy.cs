using Newtonsoft.Json;
using Services.Proxy.Abstract;
using Services.ServiceModal;
using System.Threading.Tasks;

namespace Services.Proxy
{
    public class WeatherProxy : IWeatherProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherDetail> GetWeatherByCity(string cityId)
        {           
            WeatherDetail data = null;
            var result = "";
            var client = _httpClientFactory.CreateClient();

            var task = client
                .GetAsync("forecast?id="+ cityId +"&units=metric&cnt=40&appid=e93b34b893b0c54749d7732251c107d4")
                .ContinueWith((taskresponse) =>
                {
                    var response = taskresponse.Result;
                    var jsonContent = response.Content.ReadAsStringAsync();
                    jsonContent.Wait();
                    result = jsonContent.Result;
                    data = JsonConvert.DeserializeObject<WeatherDetail>(result);

                });
            task.Wait();
            return data;
        }

        
    }
}
