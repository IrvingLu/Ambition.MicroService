using MediatR;
using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.BaseDto;

namespace NMS.Patient.Service.Patient.Command
{
    public class PatientsCommand : PageEntity, IRequest<PagedResultDto>
    {
    }
}
