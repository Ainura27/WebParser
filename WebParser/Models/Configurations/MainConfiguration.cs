using Microsoft.Extensions.Configuration;

namespace WebParser.Models.Configurations
{
    public class MainConfiguration
    {
        private readonly IConfiguration _configuration;

        public MainConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Configuration Get() => _configuration.Get<Configuration>();
    }
}
