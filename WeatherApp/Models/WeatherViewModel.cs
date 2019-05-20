using Services.ServiceModal;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WeatherApp.Models
{
    public class WeatherViewModel
    {
        //[Display(Name = "State")]
        //public string State { get; set; }
         public IList<SelectListItem> Cities { get; set; }
        public string CityId { get; set; }
        public WeatherDetail WeatherDetails { get; set; }
    }
}