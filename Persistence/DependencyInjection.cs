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
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(
                      AppSecrets.GetConnectionString(Configuration, "DefaultConnection")));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = AppSecrets.GetConnectionString(Configuration, "ConnectionStringRedis");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSession(options =>
            {
                // 20 minutes later from last access your session will be removed.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            return services;
        }
    }
}
