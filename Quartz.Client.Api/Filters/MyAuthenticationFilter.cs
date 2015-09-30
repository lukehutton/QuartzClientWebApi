using System.Configuration;
using System.Web.Http.Controllers;

namespace Quartz.Client.Api.Filters
{
    public class MyAuthenticationFilter : BasicAuthenticationFilter
    {
        public MyAuthenticationFilter() {}

        public MyAuthenticationFilter(bool active) : base(active) {}

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            // for example only
            return (ConfigurationManager.AppSettings["username"] == username &&
                    ConfigurationManager.AppSettings["password"] == password);
        }
    }
}