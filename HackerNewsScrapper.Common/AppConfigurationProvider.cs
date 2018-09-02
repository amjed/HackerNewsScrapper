using Microsoft.Extensions.Configuration;

namespace HackerNewsScrapper.Common
{
    public class AppConfigurationProvider:IAppConfigurationProvider
    {
        private readonly IConfiguration _configuration;
        public AppConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int GetIntValue(string key)
        {
            return int.Parse(_configuration[key]);
        }

        public string GetStringValue(string key)
        {
            return _configuration[key];
        }
    }
}
