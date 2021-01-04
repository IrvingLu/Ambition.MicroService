using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Domain.Abstractions.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pet.Identity.Core.Identity
{
    /// <summary>
    /// 功能描述    ：重写GenerateClaimsAsync方法， 解决sub claims is missing的问题
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/25 13:48:53 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:48:53 
    /// </summary>
    public sealed class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public ClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, roleManager, optionsAccessor)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);

            if (!identity.HasClaim(x => x.Type == JwtClaimTypes.Subject))
            {
                var sub = user.Id;
                identity.AddClaim(new Claim(JwtClaimTypes.Subject, sub));
            }
            return identity;
        }

    }
}
