using Autofac;
using Autofac.Integration.Mvc;
using Services.Manager;
using Services.Manager.Abstract;
using Services.Proxy;
using Services.Proxy.Abstract;
using Services.Repo;
using Services.Repo.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Web.Mvc;
using HttpClientFactory = Services.Proxy.HttpClientFactory;
using IHttpClientFactory = Services.Proxy.Abstract.IHttpClientFactory;

namespace WeatherApp.Configuration
{
    [ExcludeFromCodeCoverage]
    public class IoCConfig
    {
        public static IContainer RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<HttpClientFactory>().As<IHttpClientFactory>();
            builder.RegisterType<WeatherRepo>().As<IWeatherRepo>();
            builder.RegisterType<WeatherService>().As<IWeatherService>();
            builder.RegisterType<WeatherProxy>().As<IWeatherProxy>();
            IContainer container = builder.Build();
            //Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
       
    }
}