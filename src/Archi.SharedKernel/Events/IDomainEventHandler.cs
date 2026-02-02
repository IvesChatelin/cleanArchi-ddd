namespace Archi.SharedKernel.Events;

public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}
