using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Presentation.HangFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation
{
    public static class PresentationConfig
    {
        public static void UseHangFireConfiguration(this IApplicationBuilder app)
        {
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                SchedulePollingInterval = TimeSpan.FromMinutes(1),
                HeartbeatInterval = TimeSpan.FromSeconds(20),
                ServerCheckInterval = TimeSpan.FromSeconds(20),
                WorkerCount = Environment.ProcessorCount,
                ServerName = "Jobs"
            });
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });
        }

        public static void UseEndPoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        public static void UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();
        }

        public static void UseBasicConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            /*
                The X-Forwarded-For (XFF) header is a de-facto standard header for identifying the originating IP 
                address of a client connecting to a web server through an HTTP proxy or a load balancer. When traffic
                is intercepted between clients and servers, server access logs contain the IP address of the proxy or 
                load balancer only. To see the original IP address of the client, the X-Forwarded-For request header is used.
             */

            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sharp Counter V1");
            });

        }
    }
}
