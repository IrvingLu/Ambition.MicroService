using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure.Core.Extensions;

namespace NMS.Identity.Web.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="Configuration"></param>
        /// <param name="lifetime"></param>
        public static void UseConfig(this IApplicationBuilder app, IConfiguration Configuration, IHostApplicationLifetime lifetime) 
        {
            app.UseHealthChecks("/health");//健康检查
            app.UseErrorHandling();//异常处理
            app.RegisterToConsul(Configuration, lifetime);//服务注册
            app.UseIdentityServer();//id4服务
            app.UseSwaggerInfo();//Swagger
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });//api
        }
    }
}
