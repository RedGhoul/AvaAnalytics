using Hangfire;
using Microsoft.AspNetCore.Builder;
using SharpCounter.HangFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangFire
{
    public static class HangFireConfig
    {
        public static void ConfigureHangFireAuth(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = Environment.ProcessorCount,
            });

        }
    }
}
