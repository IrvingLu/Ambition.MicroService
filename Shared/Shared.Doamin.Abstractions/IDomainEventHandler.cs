using MediatR;

namespace Shared.Domain.Abstractions
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {

    }
}
