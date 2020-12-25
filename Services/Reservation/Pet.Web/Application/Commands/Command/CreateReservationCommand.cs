using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet.Reservation.Web.Application.Commands.Command
{
    public class CreateReservationCommand:IRequest
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
    }
}
