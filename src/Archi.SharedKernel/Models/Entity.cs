using Archi.SharedKernel.Events;

namespace Archi.SharedKernel.Models;

/// <summary>
/// Classe de base des entités du domaine
/// </summary>
/// <typeparam name="T">Type de l'identifiant de l'entité</typeparam>
public abstract class Entity<T> : IEntity
{
    /// <summary>
    /// Identifiant de l'entité
    /// </summary>
    public T Id { get; protected set; }

    protected Entity(T id)
    {
        Id = id;
    }

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return [.. _domainEvents];
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    } 
}
