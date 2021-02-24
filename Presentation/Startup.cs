using Application;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Swagger;

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

            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseBasicConfiguration(env);

            app.UseAuth();

            app.UseHangFireConfiguration();

            app.UseEndPoints();

            await app.UseStartupMethods();
        }
    }
}
