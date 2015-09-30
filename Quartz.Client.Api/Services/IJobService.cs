using System.Collections.Generic;
using Quartz.Client.Api.Models;

namespace Quartz.Client.Api.Services
{
    public interface IJobService
    {
        void CreateJob(CreateJobModel createJobModel);
        void TriggerJob(string jobName);
        bool CheckExists(string jobName);
        JobModel GetJob(string jobName);
        List<JobModel> GetAllJobs();
        List<JobModel> GetActiveJobs();
        void DeleteJob(string jobName);
        void DeleteAllJobs();
    }
}