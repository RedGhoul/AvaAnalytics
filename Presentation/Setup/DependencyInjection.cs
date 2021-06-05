﻿using Application.Repository;
using Domain;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.SqlServer;
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

        public static IServiceCollection UseDataStores(this IServiceCollection services, IConfiguration Configuration)
        {
            string AppDBConnectionString = AppSecrets.GetConnectionString(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                         options.UseNpgsql(AppDBConnectionString));


            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddDefaultTokenProviders()
               .AddDefaultUI()
               .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(AppDBConnectionString));


            return services;
        }

    }
}
