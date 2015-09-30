using Common.Logging;
using Quartz.Impl;

namespace Quartz.Remote.Scheduler
{
    public class MyQuartzService : IMyQuartzService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MyQuartzService));
        private IScheduler _scheduler;

        public void Start()
        {
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();

            Logger.Info("------- Initialization Complete. Starting Job Scheduler -----------");
            _scheduler.Start();
        }

        public void Stop()
        {
            _scheduler.Shutdown(false);

            Logger.Info("------- Shutdown Complete -----------------");

            var metaData = _scheduler.GetMetaData();
            Logger.InfoFormat("Executed {0} jobs.", metaData.NumberOfJobsExecuted);
        }
    }

    public interface IMyQuartzService
    {
        void Start();
        void Stop();
    }
}