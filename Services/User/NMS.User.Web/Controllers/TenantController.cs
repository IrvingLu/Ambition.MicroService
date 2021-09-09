using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMS.User.Service.Queries.Command.Tenant;
using NMS.User.Service.Tenant.Command;
using Shared.Infrastructure.Core;
using Shared.Infrastructure.Core.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NMS.User.Web.Controllers
{
    /// <summary>
    /// 功能描述    ：租户接口
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TenantController:BaseController
    {
        private readonly IMediator _mediator;

        public TenantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Post
        /// <summary>
        /// 更新租户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateTenantInfoAsync([FromBody] UpdateTenantInfoCommand command)
        {
            command.Id = Guid.Parse(User.GetTeanantId());
            await _mediator.Send(command);
            return Ok(new BaseResult((int)HttpStatusCode.OK, "Success"));
        }
        #endregion

        #region Get
        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("detail")]
        public async Task<IActionResult> GetTenantInfoAsync()
        {
            var data = await _mediator.Send(new GetTenantInfoCommand(Guid.Parse(User.GetTeanantId())));
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success",data));
        }

        #endregion

    }
}
