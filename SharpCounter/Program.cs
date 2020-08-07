using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Presentation;
using Sentry;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using SharpCounter.Config;
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

            using (SentrySdk.Init(AppSecrets.GetConnectionString(configuration, "Sentry_URL")))
            {
                Log.Logger = new LoggerConfiguration()
                   .Enrich.FromLogContext()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri($"{AppSecrets.GetConnectionString(configuration, "Log_ElasticIndexBaseUrl")}"))
                    {
                        AutoRegisterTemplate = true,
                        ModifyConnectionSettings = x => x.BasicAuthentication(AppSecrets.GetAppSettingsValue(configuration, "ELASTIC_USERNAME_Log"),
                        AppSecrets.GetAppSettingsValue(configuration, "ELASTIC_PASSWORD_Log")),
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                        IndexFormat = $"{AppSecrets.GetAppSettingsValue(configuration, "AppName")}" + "-{0:yyyy.MM}"
                    })
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
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
