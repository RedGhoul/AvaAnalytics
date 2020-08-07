﻿using Application;
using Application.Repository;
using Domain;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Presentation;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddPersistance(Configuration);
            
            services.AddApplication();

            services.AddControllersWithViews();
            
            services.AddRazorPages().AddRazorRuntimeCompilation();
            
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseBasicConfiguration(env);

            app.UseAuth();

            app.UseEndPoints();
            
            await app.CreateAdminRoleForDefaultUser();
        }
    }
}