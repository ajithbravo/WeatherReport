using Services.ServiceModal;
using System.Collections.Generic;

namespace Services.Models
{
    public class Weather
    {
        public WeatherDetail WeatherDetail { get; set; }
        public List<City> CityList { get; set; }
    }
}
