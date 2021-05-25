using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LicenseProject.Areas.Identity.IdentityHostingStartup))]
namespace LicenseProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}