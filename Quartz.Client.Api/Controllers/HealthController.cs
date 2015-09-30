using System.Web.Http;
using Quartz.Client.Api.Filters;

namespace Quartz.Client.Api.Controllers
{
    [MyAuthenticationFilter(false)]
    [RoutePrefix("health")]
    public class HealthController : ApiController
    {
        [Route(""), HttpGet]
        public IHttpActionResult HealthCheck()
        {
            return Ok("Health check OK");
        }
    }
}