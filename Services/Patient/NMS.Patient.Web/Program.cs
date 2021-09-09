/************************************************************************
*��ҳ����    ��³����
*��������    ��2020/11/10 9:51:36 
*��������    �������������
*ʹ��˵��    �������������
***********************************************************************/

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Infrastructure.Core.Core;
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
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(
                    options =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;//���
                        options.AddServerHeader = false;
                        options.Listen(IPAddress.Any, 5003);
                    });
                }).UseSerilog().UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
