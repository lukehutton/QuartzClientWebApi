using System;

namespace Quartz.Client.Api.Exceptions
{
    public class JobAlreadyExistsException : ApplicationException
    {
        public JobAlreadyExistsException(string s) : base(s)
        {
        }
    }
}