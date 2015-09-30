namespace Quartz.Client.Api.Services
{
    public interface IClientQuartzScheduler
    {
        IScheduler GetScheduler();
    }
}