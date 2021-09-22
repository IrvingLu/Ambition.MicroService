/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：中间件扩展
*使用说明    ：中间件扩展
***********************************************************************/

using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using NConsul.AspNetCore;
using Newtonsoft.Json;
using Shared.Infrastructure.Core.Utility;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// GRPC服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddConsulGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddConsul(configuration["ConsulAddress"])
                .AddGRPCHealthCheck(configuration["Consul:Address"] + ":" + Convert.ToInt32(configuration["Consul:GrpcPort"]))
                .RegisterService(configuration["Consul:Name"] + "Grpc", configuration["Consul:Address"], Convert.ToInt32(configuration["Consul:GrpcPort"]), null);
        }
        /// <summary>
        /// eventbus事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddEventBus<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            //消息总线配置
            services.AddCap(options =>
            {
                options.UseEntityFramework<TContext>();
                options.UseRabbitMQ(options =>
                {
                    options.HostName = configuration["RabbitMqAddress:HostName"];
                    options.Port = Convert.ToInt32(configuration["RabbitMqAddress:Port"]);
                    options.UserName = configuration["RabbitMqAddress:UserName"];
                    options.Password = configuration["RabbitMqAddress:Password"];
                });

                options.UseDashboard();
            });
        }
        /// <summary>
        /// 跨域配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                    policy => policy.SetIsOriginAllowed(origin => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });
        }
        /// <summary>
        /// 控制器注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddController(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //// 不使用驼峰
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }

        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddSwaggerInfo(this IServiceCollection services, string apiName)
        {
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersionEnum).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo()
                    {
                        Title = apiName,
                        Version = version,
                        Description = $"{version} 版本，可根据需要选择",
                        Contact = new OpenApiContact
                        {
                            Name = "东软医疗系统股份有限公司",
                            Email = "nms-admin@neusoftmedical.com",
                            Url = new Uri("http://www.neusoftmedical.com/")
                        },
                    });
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, apiName + ".xml");
                c.IncludeXmlComments(xmlPath, true);
            });
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            //创建配置
            var config = new TConfig();
            //绑定
            configuration.Bind(config);
            //注册
            services.AddSingleton(config);
            return config;
        }

        /// <summary>
        /// 资源服务器注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [Obsolete]
        public static void AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            //资源服务器配置
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["ApplicationConfiguration:IdentityAddress"];
                options.RequireHttpsMetadata = false;
                options.ApiName = "api";
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.TryGetValue("token", out StringValues token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var te = context.Exception;
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
