using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMS.Identity.Web.Config;
using NMS.Identity.Web.Domain;

namespace NMS.Identity.Web.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            ///上下文配置
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgresql")));
            ///身份验证配置
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
            ///认证服务器配置
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                    .AddInMemoryApiScopes(IdentityConfig.GetApiScope())
                    .AddInMemoryClients(IdentityConfig.GetClients())
                    .AddResourceOwnerValidator<PasswordValidator>()
                    .AddProfileService<ProfileService>();
            ///健康检查
            services.AddHealthChecks();
            return services;
        }
    }
}
