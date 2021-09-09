using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NMS.Patient.Infrastructure;
using Shared.Infrastructure.Core.Extensions;

namespace NMS.Patient.Web.Infrastructure.StartupExtensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 中间件配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="Configuration"></param>
        /// <param name="lifetime"></param>
        public static void UseConfig(this IApplicationBuilder app, IConfiguration Configuration, IHostApplicationLifetime lifetime)
        {
            app.UseCors("AllowSameDomain");//跨域
            app.UseAuthentication();//认证
            //app.UseAuthorization();//授权
            app.UseHealthChecks("/health");//健康检查
            app.UseApiVersioning();//api版本
            app.UseErrorHandling();//异常中间件
            app.RegisterToConsul(Configuration, lifetime);//服务注册
            app.UseSwaggerInfo();//Swagger
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });//api
            DbContextSeed.SeedAsync(app.ApplicationServices).Wait();// 初始化数据
        }
    }
}
