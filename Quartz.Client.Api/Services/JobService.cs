using System.Collections.Generic;
using Quartz.Client.Api.Exceptions;
using Quartz.Client.Api.Models;
using Quartz.Impl.Matchers;

namespace Quartz.Client.Api.Services
{
    public class JobService : IJobService
    {
        private readonly IClientQuartzScheduler _clientQuartzScheduler;

        public JobService(IClientQuartzScheduler clientQuartzScheduler)
        {
            _clientQuartzScheduler = clientQuartzScheduler;
        }

        public void DeleteJob(string jobName)
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();

            var jobKey = new JobKey(jobName, JobKey.DefaultGroup);
            if (!scheduler.CheckExists(jobKey))
            {
                throw new JobNotFoundException(string.Format("Job with Job Key {0} not found.", jobKey));
            }

            scheduler.Interrupt(jobKey);

            scheduler.DeleteJob(jobKey);
        }
        
        public void DeleteAllJobs()
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();

            foreach (var job in scheduler.GetCurrentlyExecutingJobs())
            {
                scheduler.Interrupt(job.JobDetail.Key);
            }

            scheduler.Clear();
        }

        public List<JobModel> GetActiveJobs()
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();
            var jobs = new List<JobModel>();
            foreach (var job in scheduler.GetCurrentlyExecutingJobs())
            {
                jobs.Add(ToJobModel(job.JobDetail));
            }
            return jobs;
        } 

        public JobModel GetJob(string jobName)
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();
            var jobKey = new JobKey(jobName);
            if (!scheduler.CheckExists(jobKey))
            {
                throw new JobNotFoundException(string.Format("Job with Job Key {0} not found.", jobKey));
            }
            return ToJobModel(scheduler.GetJobDetail(jobKey));
        }

        public List<JobModel> GetAllJobs()
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();
            var jobs = new List<JobModel>();
            foreach (var jobKey in GetAllJobs(scheduler))
            {
                jobs.Add(ToJobModel(scheduler.GetJobDetail(jobKey)));
            }
            return jobs;
        }     

        public bool CheckExists(string jobName)
        {
            return _clientQuartzScheduler.GetScheduler().CheckExists(new JobKey(jobName));
        }

        public void CreateJob(CreateJobModel createJobModel)
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();

            var jobKey = createJobModel.JobGroup != null ?
                new JobKey(createJobModel.JobName) :
                new JobKey(createJobModel.JobName, createJobModel.JobGroup);

            if (scheduler.CheckExists(jobKey))
            {
                throw new JobAlreadyExistsException(string.Format("Job with Job Key {0} already exists.", jobKey));
            }

            var jobBuilder = JobBuilder.Create(createJobModel.JobType)
                                .WithIdentity(jobKey)
                                .WithDescription(createJobModel.JobDescription);

            if (createJobModel.RequestRecovery)
            {
                jobBuilder = jobBuilder.RequestRecovery();
            }
            
            var job = jobBuilder.Build();
            var trigger = (ISimpleTrigger) TriggerBuilder.Create()
                .StartAt(createJobModel.ScheduledStartUtc.ToLocalTime())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public void TriggerJob(string jobName)
        {
            var scheduler = _clientQuartzScheduler.GetScheduler();
            var jobKey = new JobKey(jobName);
            if (!scheduler.CheckExists(jobKey))
            {
                throw new JobNotFoundException(string.Format("Job with Job Key {0} not found.", jobKey));
            }

            scheduler.TriggerJob(jobKey);
        }

        public static List<JobKey> GetAllJobs(IScheduler scheduler)
        {
            var jobs = new List<JobKey>();
            foreach (var group in scheduler.GetJobGroupNames())
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(@group);
                foreach (var jobKey in scheduler.GetJobKeys(groupMatcher))
                {
                    jobs.Add(jobKey);
                }
            }
            return jobs;
        }

        private List<TriggerModel> GetTriggerModels(JobKey jobKey)
        {
            var triggers = new List<TriggerModel>();
            foreach (var trigger in _clientQuartzScheduler.GetScheduler().GetTriggersOfJob(jobKey))
            {
                var triggerModel = new TriggerModel
                {
                    TriggerName = trigger.Key.Name,
                    TriggerGroup = trigger.Key.Group,
                    TriggerType = trigger.GetType().Name,
                    TriggerState = _clientQuartzScheduler.GetScheduler().GetTriggerState(trigger.Key).ToString()
                };
                var nextFireTime = trigger.GetNextFireTimeUtc();
                if (nextFireTime.HasValue)
                {
                    triggerModel.TriggerNextFireTime = nextFireTime.Value.LocalDateTime;
                }
                var previousFireTime = trigger.GetPreviousFireTimeUtc();
                if (previousFireTime.HasValue)
                {
                    triggerModel.TriggerPreviousFireTime = previousFireTime.Value.LocalDateTime;
                }
                triggers.Add(triggerModel);
            }
            return triggers;
        }

        private JobModel ToJobModel(IJobDetail jobDetail)
        {
            return new JobModel
            {
                JobGroup = jobDetail.Key.Group,
                JobName = jobDetail.Key.Name,
                JobDescription = jobDetail.Description,
                Triggers = GetTriggerModels(jobDetail.Key)
            };
        }        
    }
}