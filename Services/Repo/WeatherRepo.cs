using Services.Repo.Abstract;
using Services.Repo.Helper;
using Services.ServiceModal;
using System.Collections.Generic;

namespace Services.Repo
{
    public class WeatherRepo : IWeatherRepo
    {
        public List<City> GetAllCityList()
        {
            return ReferenceHelper.GetCityList();
        }
    }
}
