using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Services
{
    public class ConfigurationHelperService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelperService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GetConfigurationValue(string key)
        {
            return _configuration[key];
        }
    }
}
