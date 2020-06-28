using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using SharpCounter.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpCounter.Enities;
using Hangfire;
using Hangfire.PostgreSql;
using SharpCounter.HangFire;
using SharpCounter.Dapper;

namespace SharpCounter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("ConnectionStringRedis");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddHangfire(x =>
                        x.UsePostgreSqlStorage(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddHangfire(config => config
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings().UsePostgreSqlStorage(
            //    Configuration.GetConnectionString("DefaultConnection"), 
            //    new PostgreSqlStorageOptions {
            //        QueuePollInterval = TimeSpan.FromSeconds(2),
            //        InvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        DistributedLockTimeout = TimeSpan.FromMinutes(5),
            //        UseNativeDatabaseTransactions = true,
            //        PrepareSchemaIfNecessary = true,
            //        EnableTransactionScopeEnlistment = true
            //    }));
            services.AddSingleton<WebSiteRepo, WebSiteRepo>();
            services.AddSingleton<InteractionRepo, InteractionRepo>();
            services.AddSingleton<SessionRepo, SessionRepo>();
            services.AddSingleton<BrowserStatsRepo, BrowserStatsRepo>();
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 2,
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            HangFireJobScheduler.ScheduleRecurringJobs();
        }
    }
}
