using Application;
using Application.Repository;
using Domain;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Presentation;
using Presentation.Swagger;
using System;

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
            services.AddSwagger();
            services.AddResponseCompression();

            //services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseBasicConfiguration(env);

            app.UseAuth();

            //app.UseHangFireConfiguration();

            app.UseEndPoints();
            
            //await app.UseStartupMethods();
        }
    }
}
