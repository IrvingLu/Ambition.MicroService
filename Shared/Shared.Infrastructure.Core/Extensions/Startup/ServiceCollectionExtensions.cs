using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 跨域配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                    policy => policy.SetIsOriginAllowed(origin => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });
            return services;
        }
        /// <summary>
        /// 接口版本注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning((o) =>
            {
                o.ReportApiVersions = true;//可选配置，设置为true时，header返回版本信息
                o.DefaultApiVersion = new ApiVersion(1, 0);//默认版本，请求未指明版本的求默认认执行版本1.0的API
                o.AssumeDefaultVersionWhenUnspecified = true;//是否启用未指明版本API，指向默认版本
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVVV";//api组名格式
                option.AssumeDefaultVersionWhenUnspecified = true;//是否提供API版本服务
            });
            return services;
        }
        /// <summary>
        /// 控制器注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddController(this IServiceCollection services)
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
            return services;
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
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
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
            return services;
        }
    }
}
