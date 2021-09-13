/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：service注入扩展
*使用说明    ：service注入扩展
***********************************************************************/

using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure.Core.Utility;
using System;
using System.Linq;

namespace Shared.Infrastructure.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static readonly string serviceId = Guid.NewGuid().ToString();
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="Configuration"></param>
        /// <param name="lifetime"></param>
        public static void RegisterToConsul(this IApplicationBuilder app, IConfiguration Configuration, IHostApplicationLifetime lifetime)
        {

            lifetime.ApplicationStarted.Register(() =>
            {
                var serviceConfig = Configuration.GetSection("ApplicationConfiguration").GetSection("ServiceAddress");
                var client = new ConsulClient(option => option.Address = new Uri(Configuration.GetSection("ApplicationConfiguration").GetSection("ConsulAddress").Value));
                ///健康检查
                var httpCheck = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10),//服务出错1分钟之后，取消服务
                    Interval = TimeSpan.FromSeconds(10),///检查周期
                    HTTP = "http://" + serviceConfig.GetSection("Address").Value + ":" + Convert.ToInt32(serviceConfig.GetSection("Port").Value) + "/health"
                };
                ///服务注册
                var agentReg = new AgentServiceRegistration()
                {
                    ID = serviceId,
                    Check = httpCheck,
                    Address = serviceConfig.GetSection("Address").Value,
                    Port = Convert.ToInt32(serviceConfig.GetSection("Port").Value),
                    Name = serviceConfig.GetSection("Name").Value,
                };
                client.Agent.ServiceRegister(agentReg).ConfigureAwait(false);
            });

            lifetime.ApplicationStopping.Register(() =>
            {
                var client = new ConsulClient(option => option.Address = new Uri(Configuration.GetSection("ApplicationConfiguration").GetSection("ConsulAddress").Value));
                client.Agent.ServiceDeregister(serviceId).ConfigureAwait(false);
            });
        }
        /// <summary>
        /// 异常处理中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerInfo(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                typeof(ApiVersionEnum).GetEnumNames().ToList().ForEach(version =>
                {
#if DEBUG
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{version}");
#else
                    c.SwaggerEndpoint($"./swagger/{version}/swagger.json", $"{version}");
#endif
                });
            });
        }
    }
}
