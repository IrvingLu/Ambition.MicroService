using MediatR;
using Shared.Infrastructure.Core.BaseDto;

namespace Pet.User.Web.Application.Tenant.Commands.Command
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
        /// <summary>
        /// logo图
        /// </summary>
        public string LogoPath { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 公告
        /// </summary>
        public string Announcement { get; set; }
    }
}
