
using Archi.SharedKernel.Errors;

namespace Archi.Domain.FactureAggregate;

public static class FactureErrors
{
    public static readonly Error Vide = Error.Validation(
        "FactureErrors.Vide",
        "Facture vide"
    );

    public static readonly Error NullLignes = Error.Validation(
        "FactureErrors.NullLignes",
        "La facture doit contenir au moins un produit"
    );

    public static readonly Error NonModifiable = Error.Validation(
        "FactureErrors.NonModifiable",
        "La facture ne peut pas être modifiée car elle n'est pas en statut Brouillon."
    );

}
