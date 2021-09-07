using MediatR;
using NMS.User.Service.Tenant.Dto;
using Shared.Infrastructure.Core.BaseDto;
using System;

namespace NMS.User.Service.Queries.Command.Tenant
{
    /// <summary>
    /// 功能描述    ：获取租户详细信息
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public class GetTenantInfoCommand : EntityDto, IRequest<TenantInfoDto>
    {
        public GetTenantInfoCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
