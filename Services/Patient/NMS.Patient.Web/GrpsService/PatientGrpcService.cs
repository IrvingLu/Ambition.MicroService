/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：程序启动配置
*使用说明    ：程序启动配置
***********************************************************************/


using Grpc.Core;
using MediatR;
using NMS.Patient.Service.Patient.Command;
using PatientGrpcService;
using System.Threading.Tasks;

namespace NMS.Patient.Web.GrpsService
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientGrpcService : PatientGrpc.PatientGrpcBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public PatientGrpcService(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        ///获取患者详情
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<DetailResult> GetPatientInfo(DetailRequest request, ServerCallContext context)
        {
            var data = await mediator.Send(new PatientDetailCommand(System.Guid.Parse(request.Id)));
            return new DetailResult {
                Name = data.Name
            };
        }
    }
}
