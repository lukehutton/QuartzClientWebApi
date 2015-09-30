using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Common.Logging;

namespace Quartz.Client.Api
{
    public class Log4NetExceptionLogger : ExceptionLogger
    {
        private readonly ILog _log = LogManager.GetLogger(typeof (Log4NetExceptionLogger));

        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            _log.Error("An unhandled exception occurred.", context.Exception);
            await base.LogAsync(context, cancellationToken);
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _log.Error("An unhandled exception occurred.", context.Exception);
            base.Log(context);
        }
    }
}