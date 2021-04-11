using Application.Repository;
using Domain;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Presentation.Repository;
using System;
using System.Reflection;
using System.Transactions;
using Hangfire.Storage;
using Hangfire.SqlServer;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<WebSiteRepo, WebSiteRepo>();
            services.AddSingleton<InteractionRepo, InteractionRepo>();
            services.AddSingleton<SessionRepo, SessionRepo>();
            services.AddSingleton<StatsRepo, StatsRepo>();
            services.AddTransient<CacheRepo, CacheRepo>();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration Configuration)
        {
            string AppDBConnectionString = AppSecrets.GetConnectionString(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(AppDBConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddHangfire(configuration => configuration
                                       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                       .UseSimpleAssemblyNameTypeSerializer()
                                       .UseRecommendedSerializerSettings()
                                       .UseSqlServerStorage(AppDBConnectionString, new SqlServerStorageOptions
                                       {
                                           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                           QueuePollInterval = TimeSpan.Zero,
                                           UseRecommendedIsolationLevel = true,
                                           DisableGlobalLocks = true
                                       }));

            return services;
        }

    }
}
