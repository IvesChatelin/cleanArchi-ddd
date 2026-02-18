using Archi.SharedKernel.Events;

namespace Archi.SharedKernel.Models;

/// <summary>
/// Classe de base des entités du domaine
/// </summary>
/// <typeparam name="T">Type de l'identifiant de l'entité</typeparam>
public abstract class Entity<T>
{
    /// <summary>
    /// Identifiant de l'entité
    /// </summary>
    public T Id { get; protected set; }

    protected Entity(T id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    protected Entity(){}
}
