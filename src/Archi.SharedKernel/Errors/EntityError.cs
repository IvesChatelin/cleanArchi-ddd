using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Errors;

/// <summary>
/// Classe staric des erreurs liées à un domaine
/// </summary>
public static class EntityError<TEntity, TId> where TEntity : Entity<TId>
{
    public static Error NotFound(TId id) => Error.NotFound(
        "DomainError.NotFound",
        $"L'entité {typeof(TEntity).Name} avec l'identifiant {id} demandée n'existe pas."
    );

    public static Error AlreadyExists(TId id) => Error.Conflict(
        "DomainError.AlreadyExists",
        $"Cet entité {typeof(TEntity).Name} avec l'identifiant {id} existe déjà."
    );
}
