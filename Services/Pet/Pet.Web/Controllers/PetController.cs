using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pet.Web.Application.Pet.Commands.Command;
using Pet.Web.Application.Pet.Queries.Command;
using Shared.Infrastructure.Core;
using Shared.Infrastructure.Core.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace Pet.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class PetController : BaseController
    {
        private readonly IMediator _mediator;

        public PetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Post
        /// <summary>
        /// 新增宠物信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("insert")]
        public async Task<IActionResult> GetPetInfoAsync([FromBody] InsertPetCommand command)
        {
            command.ApplicationUserId = User.GetUserId();
            await _mediator.Send(command);
            return Ok(new BaseResult((int)HttpStatusCode.OK, "Success"));
        }


        #endregion

        #region query
        /// <summary>
        /// 获取我的宠物列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetPetsAsync()
        {
            var result = await _mediator.Send(new GetPetsCommand(User.GetUserId()));
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success", result));
        }

        /// <summary>
        /// 获取宠物信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Detail")]
        public async Task<IActionResult> GetPetInfoAsync([FromQuery] GetPetInfoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success", result));
        }
        #endregion



    }
}
