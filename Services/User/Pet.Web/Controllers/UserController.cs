using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pet.User.Web.Application.User.Commands.Command;
using Shared.Infrastructure.Core;
using Shared.Infrastructure.Core.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace Pet.User.Web.Controllers.User
{
    /// <summary>
    /// 功能描述    ：用户相关接口
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region 登录

        #endregion
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public async Task<IActionResult> GetUserInFOAsync()
        {
            var result = await _mediator.Send(new GetUserInfoCommand(User.GetUserId()));
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success", result));
        }
        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> WechatAsync([FromBody] WechatUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success", result));
        }


        #region 意见反馈
        /// <summary>
        /// 添加反馈意见
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("addSuggest")]
        public async Task<IActionResult> SuggestAsync([FromBody] AddSuggestCommand command)
        {
            await _mediator.Send(command);
            return Ok(new BaseResult((int)HttpStatusCode.OK, "Success"));
        }

        #endregion
    }
}
