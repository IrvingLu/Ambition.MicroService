using MediatR;
using Shared.Infrastructure.Core.BaseDto;

namespace NMS.User.Service.Tenant.Command
{
    /// <summary>
    /// 功能描述    ：更新租户的信息
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public class UpdateTenantInfoCommand:EntityDto,IRequest
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
    }
}
