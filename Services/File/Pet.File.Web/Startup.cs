using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Pet.File.Web.Application;
using Pet.File.Web.Configuration;
using Pet.File.Web.StartupExtensions;
using System.Text;

namespace Pet.File.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureStartupConfig<OssClientConfig>(Configuration.GetSection("ApplicationConfiguration").GetSection("OssConfig"));
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
            services.AddConfig(Configuration);//配置文件
            services.AddHealthChecks();//健康检查
            services.AddAuthService(Configuration);//认证服务
            services.AddSignalR();//SignalR
            services.AddApiVersion();//api版本
            services.AddController();//api控制器
            //services.AddIdentityCore<ApplicationUser>()
            //      .AddEntityFrameworkStores<ApplicationDbContext>()
            //      .AddDefaultTokenProviders();//Identity 注入
            services.AddScoped<IOssService, OssService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("AllowSameDomain");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.UseApiVersioning();
            app.RegisterToConsul(Configuration, lifetime);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<ProjectHub>("/project").RequireCors(t => t.WithOrigins(new string[] { "null" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }
    }
}
