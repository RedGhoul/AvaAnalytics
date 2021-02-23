﻿using Application.Repository;
using Domain;
using Hangfire;
using Hangfire.MySql;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Presentation.Repository;
using System;
using System.Reflection;
using System.Transactions;

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
                options.UseMySql(
                AppDBConnectionString,
                new MySqlServerVersion(new Version(8, 0, 21)),
                mySqlOptions => mySqlOptions
                    .CharSetBehavior(CharSetBehavior.NeverAppend))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();


            //services.AddHangfire(config =>
            //     config.UseStorage(new MySqlStorage(AppDBConnectionString, new MySqlStorageOptions
            //     {
            //         TransactionIsolationLevel = (System.Transactions.IsolationLevel?)IsolationLevel.Serializable,
            //         QueuePollInterval = TimeSpan.FromSeconds(15),
            //         JobExpirationCheckInterval = TimeSpan.FromMinutes(6),
            //         CountersAggregateInterval = TimeSpan.FromMinutes(6),
            //         PrepareSchemaIfNecessary = true,
            //         DashboardJobListLimit = 50000,
            //         TransactionTimeout = TimeSpan.FromMinutes(15),
            //         TablesPrefix = "Hangfire"
            //     })));

            return services;
        }

    }
}
