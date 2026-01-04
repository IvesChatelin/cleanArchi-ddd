using Archi.Application.Common.Abstractions.Events;
using Archi.Domain.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Archi.Application.Common.Behaviors;

public sealed class DomainEventsDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventsDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task DispatchAsync<TEvent>(TEvent domainEvent, CancellationToken ct) where TEvent : IDomainEvent
    {
        var handlers = _serviceProvider.GetServices<IDomainEventHandler<TEvent>>();

        foreach(var handler in handlers){
            await handler.Handle(domainEvent, ct);
        }
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach(var domainEvent in domainEvents){
            await DispatchDynamic(domainEvent, cancellationToken);
        }
    }

    private Task DispatchDynamic(
        IDomainEvent domainEvent,
        CancellationToken ct)
    {
        var method = typeof(IDomainEventDispatcher)
            .GetMethod(nameof(DispatchAsync), [domainEvent.GetType(), typeof(CancellationToken)]);

        return (Task)method!.Invoke(this, [domainEvent, ct])!;
    }
}
