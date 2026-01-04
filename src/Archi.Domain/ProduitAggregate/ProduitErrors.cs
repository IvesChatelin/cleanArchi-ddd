using Archi.Domain.Common.Errors;

namespace Archi.Domain.ProduitAggregate;

public static class ProduitErrors
{
    public static readonly Error NullName = Error.Validation(
        "ProduitErrors.NullName",
        "Nom du produit invalide"
    );

    public static Error WithParameter(int Id) => Error.Failure(
        "DomainError.WithParameter",
        $"Error related to domain with {Id} Parameter"
    );
}
