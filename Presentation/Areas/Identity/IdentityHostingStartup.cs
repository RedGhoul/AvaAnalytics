using Microsoft.AspNetCore.Hosting;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Hangfire.PostgreSql;
using Config;
using System;

[assembly: HostingStartup(typeof(SharpCounter.Areas.Identity.IdentityHostingStartup))]
namespace SharpCounter.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IConfiguration Configuration { get; }

        public IdentityHostingStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = AppSecrets.GetConnectionString(Configuration, "ConnectionStringRedis");
                });

                services.AddSession(options =>
                {
                    // 20 minutes later from last access your session will be removed.
                    options.IdleTimeout = TimeSpan.FromMinutes(20);
                });
            });
        }
    }
}