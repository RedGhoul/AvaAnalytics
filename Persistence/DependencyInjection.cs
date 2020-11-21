using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Hangfire.PostgreSql;
using Config;
using System;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration Configuration)
        {
            string AppDBConnectionString = AppSecrets.GetConnectionString(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(AppDBConnectionString));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("ConnectionStringRedis");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHangfire(config =>
                 config.UsePostgreSqlStorage(AppDBConnectionString));

            return services;
        }

    }
}
