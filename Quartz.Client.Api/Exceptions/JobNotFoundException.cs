using System;

namespace Quartz.Client.Api.Exceptions
{
    public class JobNotFoundException : ApplicationException
    {
        public JobNotFoundException(string s) : base(s)
        {
        }
    }
}