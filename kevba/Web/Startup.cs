using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Web;

[assembly: Microsoft.Owin.OwinStartup(typeof(Startup))]
namespace Web
{

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {


            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            httpConfiguration.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}