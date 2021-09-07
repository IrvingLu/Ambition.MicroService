using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Infrastructure.Core.Core;
using System.Net;

namespace NMS.User.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogConfig.ConfigureLogging();
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(
                    options =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;//½â¾ö
                        options.AddServerHeader = false;
                        options.Listen(IPAddress.Any, 5004);
                    });
                }).UseSerilog().UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
