using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SharpCounter.Areas.Identity.IdentityHostingStartup))]
namespace SharpCounter.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}