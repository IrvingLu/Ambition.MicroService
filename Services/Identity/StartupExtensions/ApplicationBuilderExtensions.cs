using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure.Core.Extensions;

namespace NMS.Identity.Web.StartupExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseConfig(this IApplicationBuilder app, IConfiguration Configuration, IHostApplicationLifetime lifetime) 
        {
            app.UseHealthChecks("/health");//健康检查
            app.UseErrorHandling();
            app.RegisterToConsul(Configuration, lifetime);//服务注册
            app.UseIdentityServer();
            app.UseSwaggerInfo();//Swagger
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });//api
        }
    }
}
