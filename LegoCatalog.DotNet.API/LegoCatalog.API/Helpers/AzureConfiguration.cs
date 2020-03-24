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
            T value;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                value = _configuration.GetValue<T>(azureKey);
                if (value != null)
                    Console.WriteLine($"Returning Azure {azureKey}:{value.ToString().Substring(0, 10)}");
            }
            else
            {
                value = _configuration.GetValue<T>(devKey);
                if (value != null)
                    Console.WriteLine($"Returning Development value {devKey}:{value.ToString().Substring(0, 10)}");
            }
            return value;
        }
    }
}