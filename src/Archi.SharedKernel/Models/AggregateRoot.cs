using Archi.SharedKernel.Events;

namespace Archi.SharedKernel.Models;

public abstract class AggregateRoot<T> : Entity<T>, IHasDomainEvent
    where T : notnull
{
    protected AggregateRoot(){}
    protected AggregateRoot(T id) : base(id){}

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return [.. _domainEvents];
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected virtual void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    } 
}
