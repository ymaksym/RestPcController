using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RestPcController
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (!WindowsServiceInstaller.TryInstall(args, out int errorCode))
            {
                CreateHostBuilder(args).Build().Run();
            }

            return errorCode;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.PreferHostingUrls(true);
                    webBuilder.UseUrls("http://*:5000");
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureServices(s => s.AddHealthChecks());
                });
    }
}
