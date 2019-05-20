using Services.Manager.Abstract;
using Services.Proxy.Abstract;
using Services.Repo.Abstract;
using Services.ServiceModal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using Services.Models;

namespace Services.Manager
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherProxy _weatherProxy;
        private readonly IWeatherRepo _weatherRepo;

        public WeatherService(IWeatherProxy weatherProxy, IWeatherRepo weatherRepo)
        {
            _weatherProxy = weatherProxy;
            _weatherRepo = weatherRepo;
        }

        public async Task<List<WeatherForecast>> GetWeatherByCity(string cityId)
        {
            var serviceResponse = await _weatherProxy.GetWeatherByCity(cityId);            
            var filterdaysquery = serviceResponse.List.GroupBy(x => Convert.ToDateTime(x.Dt_txt, CultureInfo.CurrentCulture).ToString("dd/M/yyyy", CultureInfo.InvariantCulture)).ToList().Select(x => x.Key).ToList();
            var forecast = new List<WeatherForecast>();
            foreach (var row in filterdaysquery)
            {               
                var data = serviceResponse.List.Where(x => Convert.ToDateTime(x.Dt_txt, CultureInfo.CurrentCulture).ToString("dd/M/yyyy", CultureInfo.InvariantCulture) == row.ToString());
                var min = Math.Round(data.Sum(x => x.Main.Temp_Min) / data.Count());
                var max = Math.Round(data.Sum(x => x.Main.Temp_Max) / data.Count());
                forecast.Add(new WeatherForecast { Date = row.ToString(), MinTemp = min, MaxTemp = max });
            }

            return forecast;
        }

        public List<City> GetCityList()
        {
            return _weatherRepo.GetAllCityList();
        }
    }
}
