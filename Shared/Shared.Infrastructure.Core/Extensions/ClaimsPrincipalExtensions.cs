/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：Claims扩展，用于获取claims中的对象
*使用说明    ：Claims扩展，用于获取claims中的对象
***********************************************************************/

using System;
using System.Security.Claims;

namespace Shared.Infrastructure.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(c => c.Type == "Id")?.Value;
        }
        /// <summary>
        /// 获取租户id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetTeanantId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(c => c.Type == "TenantId")?.Value;
        }
    }
}
