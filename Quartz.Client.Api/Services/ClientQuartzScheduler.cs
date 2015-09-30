using System.Collections.Specialized;
using Common.Logging;
using Quartz.Client.Api.Config;
using Quartz.Impl;

namespace Quartz.Client.Api.Services
{
    public class ClientQuartzScheduler : IClientQuartzScheduler
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (ClientQuartzScheduler));
        private readonly IScheduler _scheduler;

        public ClientQuartzScheduler(IQuartzSchedulerConfig quartzSchedulerConfig)
        {
            Address = string.Format("tcp://{0}:{1}/{2}",
                quartzSchedulerConfig.QuartzServerAddress,
                quartzSchedulerConfig.QuartzServerPort,
                quartzSchedulerConfig.QuartzSchedulerName);

            var schedulerFactory = new StdSchedulerFactory(GetProperties(Address));

            try
            {
                _scheduler = schedulerFactory.GetScheduler();
                if (!_scheduler.IsStarted)
                {
                    _scheduler.Start();
                }
            }
            catch (SchedulerException ex)
            {
                _logger.ErrorFormat("Unable to connect to Quartz server at {0}", ex, Address);
            }
        }

        public string Address { get; private set; }

        private static NameValueCollection GetProperties(string address)
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteClient";
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.threadPool.threadCount"] = "0";
            properties["quartz.scheduler.proxy.address"] = address;
            return properties;
        }

        public IScheduler GetScheduler()
        {
            return _scheduler;
        }
    }
}