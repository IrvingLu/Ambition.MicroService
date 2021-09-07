using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Core;
using System;
using System.Threading.Tasks;

namespace NMS.User.Web.Controllers.User
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
    //[Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("test")]
        public async Task<IActionResult> Index() {

            DataResult DataListResult = new();
            DataListResult = null;
           var pp= DataListResult.Data;
            return Success();

                
        }
        /// <summary>
        /// 第一种方式
        /// </summary>
        /// <param name="datetime"></param>
        //[CapSubscribe("xxx.services.show.time")]
        //public void CheckReceivedMessage(DateTime datetime)
        //{
        //    Console.WriteLine(datetime);
        //}
    }
}
