using Ninject.Modules;
using Quartz.Client.Api.Config;
using Quartz.Client.Api.Services;

namespace Quartz.Client.Api.Ninject
{
    public class MyBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfig>().To<DbConfig>();
            Bind<IQuartzSchedulerConfig>().To<QuartzSchedulerConfig>();
            Bind<IClientQuartzScheduler>().To<ClientQuartzScheduler>().InSingletonScope();
            Bind<IJobService>().To<JobService>();
        }
    }
}