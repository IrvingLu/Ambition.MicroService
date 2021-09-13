/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：程序启动入口
*使用说明    ：程序启动入口
***********************************************************************/

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Infrastructure.Core.Core;
using System;
using System.Net;

namespace NMS.Patient.Web
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
                    IConfiguration config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;//解决
                        options.AddServerHeader = false;
                        options.Listen(IPAddress.Any, Convert.ToInt32(config["ApplicationConfiguration:ServiceAddress:Port"]));
                    });
                }).UseSerilog().UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
