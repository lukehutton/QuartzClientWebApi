using System.Web.Http;
using Common.Logging;
using Quartz.Client.Api.Exceptions;
using Quartz.Client.Api.Models;
using Quartz.Client.Api.Services;

namespace Quartz.Client.Api.Controllers
{
    public class JobsController : ApiController
    {
        private readonly ILog _log;
        private readonly IJobService _jobService;

        public JobsController(ILog log, IJobService jobService)
        {
            _log = log;
            _jobService = jobService;
        }

        [Route("v1/jobs")]
        [HttpPost]
        public IHttpActionResult CreateJob([FromBody]CreateJobModel createJobModel)
        {
            try
            {
                _jobService.CreateJob(createJobModel);
            }
            catch (JobAlreadyExistsException e)
            {
                _log.Warn(e.Message);
                return Conflict();
            }

            return Ok();
        }

        [Route("v1/jobs/{jobName}/trigger")]
        [HttpPost]
        public IHttpActionResult TriggerJob(string jobName)
        {
            try
            {
                _jobService.TriggerJob(jobName);
            }
            catch (JobNotFoundException e)
            {
                _log.Warn(e.Message);
                return NotFound();
            }

            return Ok();
        }

        [Route("v1/jobs/{jobName}")]
        [HttpDelete]
        public IHttpActionResult DeleteJob(string jobName)
        {
            try
            {
                _jobService.DeleteJob(jobName);
            }
            catch (JobNotFoundException e)
            {
                _log.Warn(e.Message);
                return NotFound();
            }

            return Ok();
        }        
        
        [Route("v1/jobs")]
        [HttpDelete]
        public IHttpActionResult DeleteAllJobs()
        {
            try
            {
                _jobService.DeleteAllJobs();
            }
            catch (JobNotFoundException e)
            {
                _log.Warn(e.Message);
                return NotFound();
            }

            return Ok();
        }
        
        [Route("v1/jobs/{jobName}")]
        [HttpGet]
        public IHttpActionResult ViewJob(string jobName)
        {
            try
            {
                return Ok(_jobService.GetJob(jobName));
            }
            catch (JobNotFoundException e)
            {
                _log.Warn(e.Message);
                return NotFound();
            }
        }

        [Route("v1/jobs")]
        [HttpGet]
        public IHttpActionResult GetJobs([FromUri]bool running=false) 
        {
            return Ok(!running ? _jobService.GetAllJobs() : _jobService.GetActiveJobs());
        }
    }
}
