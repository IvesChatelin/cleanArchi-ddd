using System;
using Archi.Domain.Common.Errors;

namespace Archi.Domain.FactureAggregate;

public static class FactureErrors
{
    public static readonly Error Vide = Error.Validation(
        "FactureErrors.Vide",
        "Facture vide"
    );

}
