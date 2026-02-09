using Archi.SharedKernel.Errors;

namespace Archi.Domain.ProduitAggregate;

public static class ProduitErrors
{
    public static readonly Error NullName = Error.Validation(
        "ProduitErrors.NullName",
        "Nom du produit est null"
    );

    public static readonly Error NullPrice = Error.Validation(
        "ProduitErrors.NullPrice",
        "Le prix du produit est null"
    );

    public static readonly Error InvalidPriceUnitaireTTC = Error.Validation(
        "ProduitErrors.InvalidPriceUnitaireTTC",
        "Le prix unitaire TTC du produit est null ou inferieur ou égal à zéro"
    );
}
