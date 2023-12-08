using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public static class Config
    {
        private static IConfiguration config;

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath("G:\\dev\\repos\\CareerCloud\\CareerCloudCore.UnitTests.Assignment2")
                .AddJsonFile("appsettings.json");
            config = builder.Build();
        }
        public static string GetConnectionString()
        {
            return config.GetConnectionString("DataConnection");
        }
    }
}
