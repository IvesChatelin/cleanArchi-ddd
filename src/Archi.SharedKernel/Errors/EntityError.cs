using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Errors;

/// <summary>
/// Classe staric des erreurs liées à un domaine
/// </summary>
public static class EntityError<TEntity, TId> where TEntity : Entity<TId>
{
    public static Error NotFound(TId id) => Error.NotFound(
        "DomainError.NotFound",
        $"L'entité {typeof(TEntity).Name} avec l'identifiant {FormatId(id)} demandée n'existe pas."
    );

    public static Error AlreadyExists(TId id) => Error.Conflict(
        "DomainError.AlreadyExists",
        $"Cet entité {typeof(TEntity).Name} avec l'identifiant {FormatId(id)} existe déjà."
    );

    private static object? FormatId(TId id)
    {
        if (id is null)
            return null;

        var type = typeof(TId);

        // Cherche une propriété "Value"
        var valueProp = type.GetProperty("Value");
        if (valueProp is not null)
            return valueProp.GetValue(id);

        // Sinon fallback
        return id;
    }

}
