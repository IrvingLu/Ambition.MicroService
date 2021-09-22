using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace GateWay.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, builder) =>
            {
                builder
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("Ocelot.json");
            })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               IConfiguration config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
               webBuilder.UseStartup<Startup>();       
               webBuilder.UseKestrel(options =>   
               {
                   options.Limits.MinRequestBodyDataRate = null;//½â¾ö
                   options.AddServerHeader = false;
                   options.Listen(IPAddress.Any, Convert.ToInt32(config["ApplicationConfiguration:ServiceAddress:Port"]));
               });
           });
    }
}
