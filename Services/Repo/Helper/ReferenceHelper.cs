using Services.ServiceModal;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Services.Repo.Helper
{
    [ExcludeFromCodeCoverage]
    public static class ReferenceHelper
    {
        public static List<City> GetCityList()
        {
            var cities = new List<City>
            {
                new City
                {
                    Id=2147714,
                    Name = "Sydney"
                },
                new City
                {
                    Id=5056033,
                    Name = "London"
                },
                new City
                {
                    Id=4321929,
                    Name = "Delhi"
                }
            };

            return cities;
        }
    }
}
