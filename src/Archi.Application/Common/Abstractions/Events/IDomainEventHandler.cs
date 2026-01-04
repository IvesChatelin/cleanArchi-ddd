using Archi.Domain.Common.Abstractions;

namespace Archi.Application.Common.Abstractions.Events;

public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}
