using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientGrpcService;
using Shared.Infrastructure.Core;
using Shared.Infrastructure.Core.Grpc;
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
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly GrpcService _grpcService;

        public UserController(IMediator mediator, GrpcService grpcService)
        {
            _grpcService = grpcService;
            _mediator = mediator;
        }
        [HttpGet("test")]
        public async Task<IActionResult> Index()
        {
            var channel =await _grpcService.GetChannelByNameAsync("PatientService");
            var catClient = new PatientGrpc.PatientGrpcClient(channel);
            var data= catClient.GetPatientInfo(new DetailRequest() { Id= "32536679-fc0a-4812-95b5-fa18da74f66a" });
            return Success(data);
        }

        //[CapSubscribe("xxx.services.show.time")]
        //public void CheckReceivedMessage(DateTime datetime)
        //{
        //    Console.WriteLine(datetime);
        //}
    }
}
