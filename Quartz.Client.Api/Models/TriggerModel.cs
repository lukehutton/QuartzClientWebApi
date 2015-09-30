using System;

namespace Quartz.Client.Api.Models
{
    public class TriggerModel
    {
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string TriggerType { get; set; }
        public string TriggerState { get; set; }
        public DateTime? TriggerNextFireTime { get; set; }
        public DateTime? TriggerPreviousFireTime { get; set; }
    }
}