using System;
using System.Collections.Generic;

namespace Services.ServiceModal
{
    public class WeatherDetail
    {
        public IList<Detail> List { get; set; }
        public CityDetail City { get; set; }
    }

    public class Detail
    {
        public MainDetail Main { get; set; }
        public List<WeatherDetail> Weather { get; set; }
        public string Dt_txt { get; set; }
        public string DateTime { get; set; }
    }

    public class CityDetail
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
    public class MainDetail
    {
        public decimal Temp { get; set; }
        public decimal Temp_Min { get; set; }
        public decimal Temp_Max { get; set; }
        public decimal Pressure { get; set; }
        public decimal Sea_Level { get; set; }
        public decimal Grnd_Level { get; set; }
        public decimal Humidity { get; set; }
        public decimal Temp_kf { get; set; }
    }
    public class WeatherList
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }
}
