using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using Services.Manager.Abstract;
using Services.Models;
using Services.ServiceModal;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherApp.Controllers;
using WeatherApp.Models;

namespace WeatherApp.Tests.Controllers
{
    [TestFixture(Category = "Controller_Test")]
    public class WeatherControllerTest
    {
        private IWeatherService _weatherService;
        private WeatherController _weatherController;
        [SetUp]
        public void Init()
        {
            _weatherService = Substitute.For<IWeatherService>();
            _weatherController = new WeatherController(_weatherService);
        }
        [Test]
        public async Task GetWeatherByCity_ShouldReturn_AllWeatherDetails()
        {
            _weatherService.GetWeatherByCity(Arg.Any<string>()).ReturnsForAnyArgs(
               Task.FromResult<List<WeatherForecast>>(
                   new List<WeatherForecast> {
                   new WeatherForecast
                   {
                       Date = "19/5/2019",
                       MaxTemp = 17.89M,
                       MinTemp = 17.58M

                   }})
               );
            var response = _weatherController.GetWeatherByCountryName("2147714");
            var jason = new JavaScriptSerializer().Serialize(response.Result.Data);
            var jsonResponses = JsonConvert.DeserializeObject<List<WeatherForecast>>(jason);
            Assert.IsNotNull(response);
            Assert.AreEqual(jsonResponses.Count,1);
            Assert.AreEqual(jsonResponses[0].Date, "19/5/2019");
            Assert.AreEqual(jsonResponses[0].MaxTemp, 17.89M);
            Assert.AreEqual(jsonResponses[0].MinTemp, 17.58M);
        }
        [Test]
        public void GetCityList_ShouldLoad_AllTheCityList()
        {
            _weatherService.GetCityList().ReturnsForAnyArgs(              
                new List<City>
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
                }
              );
              var citiesresponse = _weatherController.Index() as ViewResult;
              var modle = (WeatherViewModel)citiesresponse.Model;
              Assert.AreEqual(modle.Cities.Count, 3);
              Assert.AreEqual(modle.Cities[1].Text, "London");
              Assert.AreEqual(modle.Cities[2].Value, "4321929");

        }
    }
}
