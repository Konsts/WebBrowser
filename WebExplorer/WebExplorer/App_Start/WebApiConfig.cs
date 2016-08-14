
using System.Web.Http;

namespace WebExplorer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{path}",
                defaults: new {path=RouteParameter.Optional}
            );
        }
    }
}
