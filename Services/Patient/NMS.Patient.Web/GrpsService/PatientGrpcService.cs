
using Grpc.Core;
using MediatR;
using NMS.Patient.Service.Patient.Command;
using PatientGrpcService;
using System.Threading.Tasks;

namespace NMS.Patient.Web.GrpsService
{
    public class PatientGrpcService : PatientGrpc.PatientGrpcBase
    {
        private readonly IMediator _mediator;
        public PatientGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///获取患者详情
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<DetailResult> GetPatientInfo(DetailRequest request, ServerCallContext context)
        {
            var data = await _mediator.Send(new PatientDetailCommand(System.Guid.Parse(request.Id)));
            return new DetailResult {
                Name = data.Name
            };
        }
    }
}
