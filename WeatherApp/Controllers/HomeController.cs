using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Web.Mvc;

namespace WeatherApp.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://api.openweathermap.org/data/2.5/forecast?id=2147714&APPID=f41d4731e5d6dbe8eacd49633cd456a5"); //URI  
                Console.WriteLine(Environment.NewLine + result);
            }                                  
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}