using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using NMS.File.Web.Application;
using NMS.File.Web.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace NMS.File.Web.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureStartupConfig<OssClientConfig>(configuration.GetSection("ApplicationConfiguration").GetSection("OssConfig"));
            //加载http上下文
            services.AddHttpContextAccessor();
            //解决.netcore 编码问题
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IdentityModelEventSource.ShowPII = true;//显示错误的详细信息并查看问题
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            //跨域配置
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                    policy => policy.SetIsOriginAllowed(origin => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });
            //健康检查
            services.AddHealthChecks();
            //认证服务
            services.AddAuthService(configuration);
            //api版本
            services.AddApiVersion();
            //api控制器
            services.AddController();
            ///服务注入
            services.AddScoped<IOssService, OssService>();
            return services;
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
                options.Authority = configuration.GetSection("ApplicationConfiguration").GetSection("IdentityAddress").Value;
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
    }
}
