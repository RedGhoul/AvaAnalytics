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
                 options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("ConnectionStringRedis");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHangfire(config =>
                 config.UsePostgreSqlStorage(Configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
