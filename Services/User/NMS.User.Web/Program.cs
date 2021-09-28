using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Infrastructure.Core.Core;
using System;
using System.Net;
using System.Reflection;

namespace NMS.User.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES", "SkyAPM.Agent.AspNetCore");
            LogConfig.ConfigureLogging($"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}");
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    IConfiguration config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
                    webBuilder.UseKestrel(options =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;//½â¾ö
                        options.AddServerHeader = false;
                        options.Listen(IPAddress.Any, Convert.ToInt32(config["Consul:Port"]), o => o.Protocols = HttpProtocols.Http1);
                        options.Listen(IPAddress.Any, Convert.ToInt32(config["Consul:GrpcPort"]), o => o.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog().UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
