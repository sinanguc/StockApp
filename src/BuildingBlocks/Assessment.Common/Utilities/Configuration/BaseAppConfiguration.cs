using Microsoft.Extensions.Configuration;
using System;

namespace Assessment.Common.Utilities.Configuration
{
    public class BaseAppConfiguration
    {
        public IConfigurationRoot ConfigurationRoot 
        { 
            get 
            {
                return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build(); 
            } 
        }

        public string GetRedisTokenConfig()
        {
            return ConfigurationRoot["RedisConfiguration:ConnectionString"];
        }

        public string GetTokenExpires()
        {
            return ConfigurationRoot["Token:Expires"];
        }

        public string GetConnectionString()
        {
            return ConfigurationRoot["ConnectionStrings:DefaultConnection"];
        }

        public string GetDatabaseProvider()
        {
            return ConfigurationRoot["ConnectionStrings:Provider"];
        }

        public string GetAppVersion()
        {
            return ConfigurationRoot["Version"];
        }

        public string GetApiUrl()
        {
            return ConfigurationRoot["WebAPIUrl"];
        }
    }
}
