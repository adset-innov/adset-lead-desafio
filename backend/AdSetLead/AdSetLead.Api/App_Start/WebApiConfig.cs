using AdSetLead.Data.Context;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AdSetLead.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);
           
            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(false);
            }
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );                   
        }
    }
}
