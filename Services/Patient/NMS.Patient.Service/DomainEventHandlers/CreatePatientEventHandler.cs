using NMS.Patient.Domain.Patient.Events;
using Shared.Domain.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NMS.Patient.Service.DomainEventHandlers
{
    public class CreatePatientEventHandler : IDomainEventHandler<CreatePatientEvent>
    {
        public Task Handle(CreatePatientEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
