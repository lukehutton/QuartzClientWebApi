using System;
using System.Configuration;

namespace Quartz.Client.Api.Config 
{
    public class QuartzSchedulerConfig : IQuartzSchedulerConfig
    {
        public string QuartzServerAddress
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["QuartzServerAddress"]); }
        }

        public int QuartzServerPort
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["QuartzServerPort"]); }
        }

        public string QuartzSchedulerName
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["QuartzSchedulerName"]); }
        }
    }
}