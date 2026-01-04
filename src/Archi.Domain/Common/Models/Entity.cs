using Archi.Domain.Common.Abstractions;

namespace Archi.Domain.Common.Models;

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

    public Entity(T id)
    {
        Id = id;
    }

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> ObtenirLesEvenementsDuDomaine()
    {
        return [.. _domainEvents];
    }

    public void SupprimerTousLesEvenementsDuDomaine()
    {
        _domainEvents.Clear();
    }

    protected void DeclencherUnEvenementDuDomaine(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
