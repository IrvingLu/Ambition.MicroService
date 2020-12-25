using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Extensions
{
    /// <summary>
    /// 功能描述    ：ClaimsPrincipalExtensions  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 15:31:26 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 15:31:26 
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(c => c.Type == "Id")?.Value;
        }
    }
}
