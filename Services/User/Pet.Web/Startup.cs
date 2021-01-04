using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Pet.User.Infrastructure;
using Pet.User.Web.Infrastructure;
using Pet.User.Web.Infrastructure.StartupExtensions;
using Shared.Domain.Abstractions.Identity;
using System.Text;

namespace Pet.User.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            ////redis����
            var section = Configuration.GetSection("ApplicationConfiguration").GetSection("WechatConfig");
            RedisHelper.Initialization(new CSRedis.CSRedisClient(Configuration.GetConnectionString("CsRedisCachingConnectionString")));
            //AccessTokenContainer.RegisterAsync(section.GetSection("AppId").Value, section.GetSection("AppSecret").Value).ConfigureAwait(false).GetAwaiter().GetResult();
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
          
            ////����hangfire��ʱ����
            //services.AddHangfire(x => x.UseStorage(new MySqlStorage(Configuration.GetConnectionString("MySql") + ";Allow User Variables=true", new MySqlStorageOptions
            //{
            //    TablePrefix = "Hangfire"
            //})));

            services.AddConfig(Configuration);//�����ļ�
            services.AddApplicationDbContext(Configuration);//DbContext������
            services.AddIdentityOptions();//�����֤����
            services.AddAutoMapper(typeof(Startup));//automapper
            services.AddMediatR(typeof(Startup));//CQRS
            services.AddHealthChecks();//�������
            //services.AddEventBus(Configuration);//�¼�����
            services.AddAuthService(Configuration);//��֤����
            services.AddSignalR();//SignalR
            services.AddApiVersion();//api�汾
            services.AddController();//api������
            services.AddIdentityCore<ApplicationUser>()
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();//Identity ע��
        }
        /// <summary>
        /// �м���ܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
            app.UseLog4net();
            app.UseErrorHandling();
            app.RegisterToConsul(Configuration, lifetime);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<ProjectHub>("/project").RequireCors(t => t.WithOrigins(new string[] { "null" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            //app.UseHangfireServer(new BackgroundJobServerOptions
            //{
            //    WorkerCount = 1
            //});
            RegisterJobs();
            //��ʼ������
            DbContextSeed.SeedAsync(app.ApplicationServices).Wait();
        }

        /// <summary>
        /// ��ʱ����
        /// </summary>
        private static void RegisterJobs()
        {
            //15
           // RecurringJob.AddOrUpdate<IHanfireTaskService>("OrderReceivedConfirmAuto", job => job.OrderReceivedConfirmAuto(15), Cron.Minutely);//"0 */1 * * * ?"
        }
        /// <summary>
        /// autofac����ע��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DependencyRegistrar());
        }
    }

}
