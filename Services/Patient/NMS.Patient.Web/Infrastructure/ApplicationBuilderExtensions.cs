/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：管道扩展
*使用说明    ：管道扩展
***********************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NMS.Patient.Infrastructure;
using Shared.Infrastructure.Core.Extensions;
using Shared.Infrastructure.Core.GrpcService;

namespace NMS.Patient.Web.Infrastructure
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
            app.UseHealthChecks("/health");//健康检查
            app.UseApiVersioning();//api版本
            app.UseErrorHandling();//异常中间件
            app.RegisterToConsul(Configuration, lifetime);//服务注册
            app.UseSwaggerInfo();//Swagger
            //Api&&Grpc
            app.UseRouting();
            app.UseEndpoints(endpoints => { 
                endpoints.MapControllers();
                endpoints.MapGrpcService<HealthCheckService>();
                endpoints.MapGrpcService<GrpsService.PatientGrpcService>(); 
            });
            DbContextSeed.SeedAsync(app.ApplicationServices).Wait();// 初始化数据
        }
    }
}
