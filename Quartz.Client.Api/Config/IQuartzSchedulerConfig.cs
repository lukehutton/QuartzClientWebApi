namespace Quartz.Client.Api.Config
{
    public interface IQuartzSchedulerConfig
    {
        string QuartzServerAddress { get; }
        int QuartzServerPort { get; }
        string QuartzSchedulerName { get; }
    }
}