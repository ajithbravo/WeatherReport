using Services.ServiceModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Proxy.Abstract
{
    public interface IWeatherProxy
    {
        Task<WeatherDetail> GetWeatherByCity(string cityId);
    }
}
