using Services.Manager.Abstract;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public ActionResult Index()
        {
            var response = _weatherService.GetCityList();
            var weather = new WeatherViewModel
            {
                Cities = response.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };

            return View(weather);
        }
        public async Task<JsonResult> GetWeatherByCountryName(string id)
        {
            var weather = await _weatherService.GetWeatherByCity(id);
            return Json(weather, JsonRequestBehavior.AllowGet);
        }     
          
                 
    }
}
