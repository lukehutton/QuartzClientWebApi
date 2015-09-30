using System.Security.Principal;

namespace Quartz.Client.Api.Security
{
    /// <summary>
    /// Custom Identity that adds a password captured by basic authentication
    /// to allow for an auth filter to do custom authorization
    /// </summary>
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(string name, string password)
            : base(name, "Basic")
        {
            Password = password;
        }

        /// <summary>
        /// Basic Auth Password for custom authentication
        /// </summary>
        public string Password { get; set; }
    }
}
