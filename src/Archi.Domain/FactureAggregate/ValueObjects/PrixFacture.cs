using Archi.Domain.Common.Models;

namespace Archi.Domain.FactureAggregate.ValueObjects;

public sealed class PrixFacture : ValueObject
{
    public decimal MontantTotalHt { get; }

    public decimal MontantTotalAPayer { get; }

    public decimal TotalTvaEur { get; }

    public PrixFacture(decimal montantTotalAPayer, decimal totalTvaEur)
    {
        MontantTotalHt = montantTotalAPayer - totalTvaEur;
        MontantTotalAPayer = montantTotalAPayer;
        TotalTvaEur = totalTvaEur;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return MontantTotalHt;
        yield return MontantTotalAPayer;
        yield return TotalTvaEur;
    }
}
