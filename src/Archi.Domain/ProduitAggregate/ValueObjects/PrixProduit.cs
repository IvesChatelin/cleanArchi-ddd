using Archi.Domain.Common.Models;

namespace Archi.Domain.ProduitAggregate.ValueObjects;

public sealed class PrixProduit : ValueObject
{
    public required decimal PrixUnitaireHt { get; init; }

    public decimal PrixUnitaireTTC { get; }

    public required decimal TvaEur { get; init; }

    public uint TvaPourcentage { get; }

    public PrixProduit(decimal prixUnitaireHt, decimal tvaEur)
    {
        PrixUnitaireHt = prixUnitaireHt;
        PrixUnitaireTTC = prixUnitaireHt + tvaEur;
        TvaEur = tvaEur;
        TvaPourcentage = (uint)Math.Ceiling(tvaEur / prixUnitaireHt * 100);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return PrixUnitaireHt;
        yield return PrixUnitaireTTC;
        yield return TvaEur;
        yield return TvaPourcentage;
    }
}
