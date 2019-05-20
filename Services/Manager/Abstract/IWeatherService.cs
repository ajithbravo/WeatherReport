using Services.Models;
using Services.ServiceModal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Manager.Abstract
{
    public interface IWeatherService
    {
        Task<List<WeatherForecast>> GetWeatherByCity(string cityId);
        List<City> GetCityList();
        
    }
}
