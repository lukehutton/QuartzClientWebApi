using System.Reflection;
using System.Web;
using System.Web.Http;
using Ninject;
using WebApiContrib.IoC.Ninject;

namespace Quartz.Client.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
        }
    }
}