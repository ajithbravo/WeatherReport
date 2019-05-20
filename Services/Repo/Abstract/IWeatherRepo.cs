using Services.ServiceModal;
using System.Collections.Generic;

namespace Services.Repo.Abstract
{
    public interface IWeatherRepo
    {
        List<City> GetAllCityList();        
    }
}
