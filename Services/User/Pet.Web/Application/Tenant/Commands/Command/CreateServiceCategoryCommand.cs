using MediatR;
using System;

namespace Pet.User.Web.Application.Tenant.Commands.Command
{
    /// <summary>
    /// 功能描述    ：创建租户的服务
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public class CreateServiceCategoryCommand:IRequest
    {
        /// <summary>
        /// 服务类目
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 市场平均价格
        /// </summary>
        public int MarketPrice { get; set; }
        /// <summary>
        /// 店铺平均价格
        /// </summary>
        public int SalePrice { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid TenantId { get; set; }

    }
}
