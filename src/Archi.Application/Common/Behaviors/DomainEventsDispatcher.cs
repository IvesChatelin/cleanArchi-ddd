using Archi.SharedKernel.Events;
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
        // On récupère la méthode générique ouverte
        var method = typeof(IDomainEventDispatcher)
            .GetMethods()
            .First(m => m.Name == nameof(DispatchAsync) && m.IsGenericMethod);

        // On la ferme avec le type réel de l’event
        var genericMethod = method.MakeGenericMethod(domainEvent.GetType());

        // On invoque la bonne version
        return (Task)genericMethod.Invoke(this, [domainEvent, ct])!;
    }
}
