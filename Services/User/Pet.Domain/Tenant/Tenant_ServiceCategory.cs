using Shared.Domain.Abstractions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.User.Domain.Tenant
{
    /// <summary>
    /// 功能描述    ：租户的服务类目  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 14:32:56 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 14:32:56 
    /// </summary>
    public class Tenant_ServiceCategory:Entity
    {
        #region Base
        /// <summary>
        /// 服务类目
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务详情
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 市场平均价格
        /// </summary>
        public int MarketPrice { get; set; }
        /// <summary>
        /// 店铺平均价格
        /// </summary>
        public int SalePrice { get; set; }
        #endregion
        /// <summary>
        /// 所属租户
        /// </summary>
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
