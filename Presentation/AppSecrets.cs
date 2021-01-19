
using System;
using Microsoft.Extensions.Configuration;
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
                //Console.WriteLine(e);
                //Console.WriteLine("Could not find it in the Configuration");
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
                //Console.WriteLine(e);
                //Console.WriteLine("Could not find it in the Configuration");
                Console.WriteLine("using the following value instead: " + Environment.GetEnvironmentVariable(name));
            }

            return Environment.GetEnvironmentVariable(name);
        }

        public static string GetConnectionString(IConfiguration Configuration)
        {
            string AppDBConnectionString;
            if (GetAppSettingsValue(Configuration,"Environment").Equals("Dev"))
            {
                AppDBConnectionString = GetConnectionString(Configuration,"AvaAnalytics_Local");
            }
            else
            {
                
                AppDBConnectionString = GetConnectionString(Configuration, "AvaAnalytics_Prod");
                
            }

            return AppDBConnectionString;
        }
    }
}
