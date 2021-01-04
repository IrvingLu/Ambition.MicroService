using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pet.Identity.Core.Identity;
using Pet.Identity.Infrastructure;
using Pet.Identity.Web.StartupExtensions;
using Shared.Domain.Abstractions.Identity;
using System;

namespace Pet.Identity.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            ///����������
            services.AddDbContext<ApplicationDbContext>(
               options => options
                   .UseMySql(Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 21))),ServiceLifetime.Transient);
            //�����֤����
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
            //��֤����������
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                    .AddInMemoryApiScopes(IdentityConfig.GetApiScope())
                    .AddInMemoryClients(IdentityConfig.GetClients())
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    .AddProfileService<ProfileService>();
            ///�������
            services.AddHealthChecks();
            ///�汾����
            services.AddApiVersioning((o) =>
            {
                o.ReportApiVersions = true;//��ѡ���ã�����Ϊtrueʱ��header���ذ汾��Ϣ
                o.DefaultApiVersion = new ApiVersion(1, 0);//Ĭ�ϰ汾������δָ���汾����Ĭ����ִ�а汾1.0��API
                o.AssumeDefaultVersionWhenUnspecified = true;//�Ƿ�����δָ���汾API��ָ��Ĭ�ϰ汾
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVVV";//api������ʽ
                option.AssumeDefaultVersionWhenUnspecified = true;//�Ƿ��ṩAPI�汾����
            });
            ///client
            services.AddHttpClient();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UseRouting();
            app.UseAuthentication();//�ô�����Ҫ�ȼ���
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.RegisterToConsul(Configuration, lifetime);
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });
        }

    }
}
