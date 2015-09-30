using System;

namespace Quartz.Client.Api.Models
{
    public class CreateJobModel
    {
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public Type JobType { get; set; }
        public bool RequestRecovery { get; set; }
        public DateTime ScheduledStartUtc { get; set; }
    }
}