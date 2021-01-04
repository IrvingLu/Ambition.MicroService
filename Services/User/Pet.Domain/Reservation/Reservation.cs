using Shared.Domain.Abstractions;
using System;
namespace Pet.Reservation.Domain.Reservation
{
    /// <summary>
    /// 功能描述    ：预约表  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public class Reservation : Entity, IAggregateRoot
    {
        /// <summary>
        /// 预约用户id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 服务类目
        /// </summary>
        public Guid Tenant_ServiceCategoryId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public Guid TenantId { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }

        public Reservation()
        {

        }
        public Reservation(Guid userId, Guid tenant_ServiceCategoryId, Guid tenantId, DateTime reserveTime)
        {
            this.UserId = userId;
            this.Tenant_ServiceCategoryId = tenant_ServiceCategoryId;
            this.TenantId = tenantId;
            this.ReserveTime = reserveTime;
        }
    }
}
