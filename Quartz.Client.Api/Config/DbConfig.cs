using System.Configuration;

namespace Quartz.Client.Api.Config 
{
    public class DbConfig : IDbConfig
    {
        public string DatabaseConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString; }
        }
    }
}