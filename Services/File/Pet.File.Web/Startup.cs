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
            //����http������
            services.AddHttpContextAccessor();
            //���.netcore ��������
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IdentityModelEventSource.ShowPII = true;//��ʾ�������ϸ��Ϣ���鿴����
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            //��������
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                    policy => policy.SetIsOriginAllowed(origin => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });
            services.AddConfig(Configuration);//�����ļ�
            services.AddHealthChecks();//�������
            services.AddAuthService(Configuration);//��֤����
            services.AddSignalR();//SignalR
            services.AddApiVersion();//api�汾
            services.AddController();//api������
            //services.AddIdentityCore<ApplicationUser>()
            //      .AddEntityFrameworkStores<ApplicationDbContext>()
            //      .AddDefaultTokenProviders();//Identity ע��
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
