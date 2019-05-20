using NSubstitute;
using NUnit.Framework;
using Services.Manager;
using Services.Proxy.Abstract;
using Services.Repo.Abstract;
using Services.ServiceModal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Tests.Service
{

    [TestFixture(Category = "Service_Test")]
    public class WeatherServiceTest
    {
        private IWeatherProxy _weatherProxy;
        private IWeatherRepo _weatherRepo;
        private WeatherService _weatherService;

        [SetUp]
        public void Init()
        {
            _weatherProxy = Substitute.For<IWeatherProxy>();
            _weatherRepo = Substitute.For<IWeatherRepo>();
            _weatherService = new WeatherService(_weatherProxy, _weatherRepo);
        }
        [Test]
        public async Task GetWeatherByCity_ShouldReturn_Temperature_and_Date()
        {

            _weatherProxy.GetWeatherByCity(Arg.Any<string>()).ReturnsForAnyArgs(
                Task.FromResult<WeatherDetail>(
                    new WeatherDetail
                    {
                        City = new CityDetail { Country = "AU", Name = "Sydney" },
                        List = new List<Detail> {
                            new Detail {
                                DateTime = "19/5/2019",
                                Dt_txt = "2019-05-19 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 17.89M,
                                    Temp_Min=17.58M
                                }
                            } }
                    })
                );

            var response = await _weatherService.GetWeatherByCity("2147714");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 1);
            Assert.AreEqual(response[0].Date, "19/5/2019");
            Assert.AreEqual(response[0].MaxTemp, 18M);
            Assert.AreEqual(response[0].MinTemp, 18M);

        }

        [Test]
        public async Task GetWeatherByCity_ShouldReturn_Returns_Date()
        {

            _weatherProxy.GetWeatherByCity(Arg.Any<string>()).ReturnsForAnyArgs(
                Task.FromResult<WeatherDetail>(
                    new WeatherDetail
                    {
                        City = new CityDetail { Country = "AU", Name = "Sydney" },
                        List = new List<Detail> {
                            new Detail {
                                Dt_txt = "2019-05-19 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 17.89M,
                                    Temp_Min=17.58M
                                }
                            } }
                    })
                );

            var response = await _weatherService.GetWeatherByCity("2147714");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 1);
            Assert.AreEqual(response[0].Date, "19/5/2019");
            Assert.AreEqual(response[0].MaxTemp, 18M);
            Assert.AreEqual(response[0].MinTemp, 18M);

        }
        [Test]
        public void GetCityList_ShouldReturnAllCityList()
        {
            _weatherRepo.GetAllCityList().ReturnsForAnyArgs(
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

            var response = _weatherService.GetCityList();
            Assert.AreEqual(response.Count, 3);
            Assert.AreEqual(response[2].Name, "Delhi");
            Assert.AreNotEqual(response[1].Id, 1213);
            Assert.AreEqual(response[1].Id, 5056033);
        }

        [Test]
        public async Task GetWeatherByCity_ShouldReturn_Retrun_AverageMaxTemp()
        {

            _weatherProxy.GetWeatherByCity(Arg.Any<string>()).ReturnsForAnyArgs(
                Task.FromResult<WeatherDetail>(
                    new WeatherDetail
                    {
                        City = new CityDetail { Country = "AU", Name = "Sydney" },
                        List = new List<Detail>
                        {
                            new Detail {
                                Dt_txt = "2019-05-19 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 40M,
                                    Temp_Min=30M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-19 10:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 20M,
                                    Temp_Min=10M
                                }
                            }
                        }
                    }

                    )
                );

            var response = await _weatherService.GetWeatherByCity("2147714");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 1);
            Assert.AreEqual(response[0].Date, "19/5/2019");
            Assert.AreEqual(response[0].MaxTemp, 30M);
            Assert.AreEqual(response[0].MinTemp, 20M);

        }

        [Test]
        public async Task GetWeatherByCity_ShouldReturn_Retrun_FiveDaysWeather()
        {

            _weatherProxy.GetWeatherByCity(Arg.Any<string>()).ReturnsForAnyArgs(
                Task.FromResult<WeatherDetail>(
                    new WeatherDetail
                    {
                        City = new CityDetail { Country = "AU", Name = "Sydney" },
                        List = new List<Detail>
                        {
                            new Detail {
                                Dt_txt = "2019-05-19 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 40M,
                                    Temp_Min=30M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-19 10:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 20M,
                                    Temp_Min=10M
                                }
                            },
                             new Detail {
                                Dt_txt = "2019-05-20 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 18M,
                                    Temp_Min=12M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-20 10:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 33M,
                                    Temp_Min=13M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-21 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 12M,
                                    Temp_Min=6M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-21 10:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 10M,
                                    Temp_Min=3M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-22 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 35M,
                                    Temp_Min=15M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-22 10:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 25M,
                                    Temp_Min=10M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-23 09:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 40M,
                                    Temp_Min=12M
                                }
                            },
                            new Detail {
                                Dt_txt = "2019-05-23 10:00:00",
                                Main= new MainDetail
                                {
                                    Temp_Max= 23M,
                                    Temp_Min=16M
                                }
                            }
                        }
                    }

                    )
                );

            var response = await _weatherService.GetWeatherByCity("2147714");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 5);
            Assert.AreEqual(response[3].Date, "22/5/2019");
            Assert.AreEqual(response[4].MaxTemp, 32M);
            Assert.AreEqual(response[4].MinTemp, 14M);

        }
    }
}
