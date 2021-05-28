
using Microsoft.Extensions.Configuration;
using System;
namespace Application
{
    public static class AppSecrets
    {
        public static string GetAppSettingsValue(IConfiguration Configuration, string name)
        {
            try
            {
                string value = Configuration.GetSection("AppSettings")[name];
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("using the following value instead: " + Environment.GetEnvironmentVariable(name));
            }

            return Environment.GetEnvironmentVariable(name);
        }

        public static string GetConnectionString(IConfiguration Configuration, string name)
        {
            try
            {
                string value = Configuration.GetConnectionString(name);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("using the following value instead: " + Environment.GetEnvironmentVariable(name));
            }

            return Environment.GetEnvironmentVariable(name);
        }

        public static string GetConnectionString(IConfiguration Configuration)
        {
            string AppDBConnectionString;
            if (GetAppSettingsValue(Configuration, "Environment").Equals("Dev"))
            {
                AppDBConnectionString = GetConnectionString(Configuration, "AvaAnalytics_Local");
            }
            else
            {

                AppDBConnectionString = GetConnectionString(Configuration, "AvaAnalytics_Prod");

            }

            return AppDBConnectionString;
        }

        public static string GetHangfireConnectionString(IConfiguration configuration)
        {
            string AppDBConnectionString;
            if (configuration.GetValue<string>("Environment").Equals("Dev"))
            {
                AppDBConnectionString = GetConnectionString(configuration, "AvaAnalytics_Prod");
            }
            else
            {
                AppDBConnectionString = GetConnectionString(configuration, "AvaAnalytics_Prod_Hangfire");
            }

            return AppDBConnectionString;
        }
    }
}
