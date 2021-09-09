using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NMS.User.Infrastructure;
using Shared.Infrastructure.Core.Extensions;

namespace NMS.User.Web.Infrastructure.StartupExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseConfig(this IApplicationBuilder app, IConfiguration Configuration, IHostApplicationLifetime lifetime)
        {
            app.UseCors("AllowSameDomain");//跨域
            app.UseAuthentication();//认证
            app.UseHealthChecks("/health");//健康检查
            app.UseErrorHandling();//异常中间件
            app.RegisterToConsul(Configuration, lifetime);//服务注册
            app.UseSwaggerInfo();//Swagger
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });//api
            DbContextSeed.SeedAsync(app.ApplicationServices).Wait();// 初始化数据
        }
    }
}
