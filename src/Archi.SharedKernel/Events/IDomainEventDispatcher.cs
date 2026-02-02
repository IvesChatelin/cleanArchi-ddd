namespace Archi.SharedKernel.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync<TEvent>(
        TEvent domainEvent,
        CancellationToken ct)
        where TEvent : IDomainEvent;

    Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default);
}
