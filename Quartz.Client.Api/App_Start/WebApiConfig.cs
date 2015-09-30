using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Quartz.Client.Api.Filters;

namespace Quartz.Client.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Services.Add(typeof (IExceptionLogger), new Log4NetExceptionLogger());

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );

            config.Filters.Add(new MyAuthenticationFilter());
        }
    }
}