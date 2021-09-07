using MediatR;
using Shared.Infrastructure.Core.BaseDto;

namespace NMS.Patient.Service.Patient.Command
{
    public class CreatePatientCommand : EntityDto, IRequest
    {
        public string Name { get; set; }
    }
}
