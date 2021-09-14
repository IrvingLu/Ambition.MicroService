/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：服务扩展
*使用说明    ：服务扩展
***********************************************************************/

using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using NMS.Patient.Infrastructure;
using NMS.Patient.Service;
using NMS.Patient.Service.IntegrationEvents;
using Shared.Infrastructure.Core.Extensions;
using System;
using System.Text;

namespace NMS.Patient.Web.Infrastructure.StartupExtensions
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            RedisHelper.Initialization(new CSRedis.CSRedisClient(configuration.GetConnectionString("CsRedisCachingConnectionString")));//redis初始化
            services.AddHttpContextAccessor();//加载http上下文
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//解决.netcore 编码问题
            IdentityModelEventSource.ShowPII = true;//显示错误的详细信息并查看问题
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddCorsConfig();//跨域配置
            services.AddConfig(configuration);//配置文件
            services.AddDbContext<ApplicationDbContext>();//DbContext上下文
            services.AddAutoMapper(typeof(ServiceStartup));//automapper
            services.AddMediatR(typeof(ServiceStartup));//CQRS
            services.AddHealthChecks();//健康检查
            services.AddEventBus(configuration);//事件总线
            services.AddController();//api控制器
            services.AddSwaggerInfo($"{typeof(Startup).Namespace}");
            return services;
        }
        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.ConfigureStartupConfig<MongodbHostConfig>(configuration.GetSection("MongodbHostConfig"));
        }

        /// <summary>
        /// eventbus事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISubscriberService, SubscriberService>();
            var mqconfig = configuration.GetSection("ApplicationConfiguration").GetSection("RabbitMqAddress");
            //消息总线配置
            services.AddCap(options =>
            {
                options.UseEntityFramework<ApplicationDbContext>();
                options.UseRabbitMQ(options =>
                {
                    options.HostName = mqconfig.GetSection("HostName").Value;
                    options.Port = Convert.ToInt32(mqconfig.GetSection("Port").Value);
                    options.UserName = mqconfig.GetSection("UserName").Value;
                    options.Password = mqconfig.GetSection("Password").Value;
                });

                options.UseDashboard();
            });
        }    
    }
}
