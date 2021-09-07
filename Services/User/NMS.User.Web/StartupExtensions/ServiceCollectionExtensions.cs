using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using NMS.User.Domain.Identity;
using NMS.User.Infrastructure;
using NMS.User.Service;
using NMS.User.Service.IntegrationEvents;
using Shared.Infrastructure.Core.Extensions;
using System;
using System.Text;

namespace NMS.User.Web.Infrastructure.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                    policy => policy.SetIsOriginAllowed(origin => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });//跨域配置
            services.AddConfig(configuration);//配置文件
            services.AddIdentityOptions();//身份认证配置
            services.AddAutoMapper(typeof(Startup));//automapper
            services.AddMediatR(typeof(ServiceStartup));//CQRS
            services.AddHealthChecks();//健康检查
            services.AddEventBus(configuration);//事件总线
            services.AddAuthService(configuration);//认证服务
            services.AddApiVersion();//api版本
            services.AddController();//api控制器
            services.AddIdentity<ApplicationUser,ApplicationRole>()
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();//Identity  usermanager和rolemanager服务注入
            return services;

        }
        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.ConfigureStartupConfig<MongodbHostConfig>(configuration.GetSection("MongodbHostConfig"));
            return services;
        }
        /// <summary>
        /// eventbus事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<ISubscriberService, SubscriberService>();
            var mqconfig = configuration.GetSection("ApplicationConfiguration").GetSection("RabbitMqAddress");
            ///消息总线配置
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

            return services;
        }
        /// <summary>
        /// 身份认证配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        {

            services.Configure<IdentityOptions>(options =>
            {
                // 密码配置
                options.Password.RequireDigit = false;//是否需要数字(0-9).
                options.Password.RequiredLength = 6;//设置密码长度最小为6
                options.Password.RequireNonAlphanumeric = false;//是否包含非字母或数字字符。
                options.Password.RequireUppercase = false;//是否需要大写字母(A-Z).
                options.Password.RequireLowercase = false;//是否需要小写字母(a-z).
                // 锁定设置
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);//账户锁定时长30分钟
                options.Lockout.MaxFailedAccessAttempts = 10;//10次失败的尝试将账户锁定
                options.Lockout.AllowedForNewUsers = false;
                // 用户设置
                options.User.RequireUniqueEmail = false; //是否Email地址必须唯一
            });
            return services;
        }
    }
}
