using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Config;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Hangfire.MySql.Core;
using System.Transactions;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration Configuration)
        {
            string AppDBConnectionString = AppSecrets.GetConnectionString(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                // Replace with your connection string.
                AppDBConnectionString,
                // Replace with your server version and type.
                // For common usages, see pull request #1233.
                new MySqlServerVersion(new Version(8, 0, 21)), // use MariaDbServerVersion for MariaDB
                mySqlOptions => mySqlOptions
                    .CharSetBehavior(CharSetBehavior.NeverAppend))
            // Everything from this point on is optional but helps with debugging.
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("ConnectionStringRedis");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHangfire(config =>
                  config.UseStorage(new MySqlStorage(AppDBConnectionString, new MySqlStorageOptions
                  {
                      TransactionIsolationLevel = (System.Data.IsolationLevel?)IsolationLevel.ReadCommitted,
                      QueuePollInterval = TimeSpan.FromSeconds(15),
                      JobExpirationCheckInterval = TimeSpan.FromHours(1),
                      CountersAggregateInterval = TimeSpan.FromMinutes(5),
                      PrepareSchemaIfNecessary = true,
                      DashboardJobListLimit = 50000,
                      TransactionTimeout = TimeSpan.FromMinutes(1),
                      TablePrefix = "Hangfire"
                  })));

            return services;
        }

    }
}
