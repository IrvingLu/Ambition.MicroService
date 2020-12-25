
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Pet.Web.Infrastructure.StartupExtensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用log4net配置
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLog4net(this IApplicationBuilder app)
        {
            var logRepository = log4net.LogManager.CreateRepository(System.Reflection.Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            return app;
        }
        private static readonly string serviceId = Guid.NewGuid().ToString();
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="Configuration"></param>
        /// <param name="lifetime"></param>
        public static void RegisterToConsul(this IApplicationBuilder app, IConfiguration Configuration, IHostApplicationLifetime lifetime)
        {

            lifetime.ApplicationStarted.Register(() => {

                var serviceConfig = Configuration.GetSection("ApplicationConfiguration").GetSection("SerivceAddress");
                var client = new ConsulClient(option => option.Address = new Uri(Configuration.GetSection("ApplicationConfiguration").GetSection("ConsulAddress").Value));
                ///健康检查
                var httpCheck = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10),//服务出错1分钟之后，取消服务
                    Interval = TimeSpan.FromSeconds(10),///检查周期
                    HTTP = serviceConfig.GetSection("HttpType").Value + serviceConfig.GetSection("Address").Value + ":" + Convert.ToInt32(serviceConfig.GetSection("Port").Value) + "/health"
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
    }
}
