using System;
using Microsoft.Extensions.Configuration;

namespace LegoCatalog.API.Helpers
{
    public class AzureConfiguration
    {
        private IConfiguration _configuration;

        public AzureConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetValue<T>(string devKey, string azureKey) 
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                return _configuration.GetValue<T>(azureKey);
            else
                return _configuration.GetValue<T>(devKey);
        }
    }
}