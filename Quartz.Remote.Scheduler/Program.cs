using System;
using Common.Logging;
using Topshelf;

namespace Quartz.Remote.Scheduler
{
    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (Program));

        private static void Main(string[] args)
        {
            try
            {
                HostFactory.Run(x =>
                {
                    x.Service<MyQuartzService>(s =>
                    {
                        s.ConstructUsing(name => new MyQuartzService());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalSystem();

                    x.SetDescription("MyQuartzService");
                    x.SetDisplayName("MyQuartzService");
                    x.SetServiceName("MyQuartzService");
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}