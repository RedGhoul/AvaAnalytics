using Application;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Presentation;
using Serilog;
using System;

namespace SharpCounter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = null;
            try
            {
                configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            string AppDBConnectionString = AppSecrets.GetConnectionString(configuration);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MySQL(AppDBConnectionString, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .CreateLogger();
            
            try
            {
                Log.Information("Starting up");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
