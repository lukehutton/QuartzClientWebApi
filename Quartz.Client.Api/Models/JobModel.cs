using System.Collections.Generic;

namespace Quartz.Client.Api.Models
{
    public class JobModel
    {
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public List<TriggerModel> Triggers { get; set; }
    }
}