using Archi.SharedKernel.Models;

namespace Archi.Domain.ProduitAggregate.ValueObjects;

public sealed class PrixProduit : ValueObject
{
    public decimal PrixUnitaireHt { get; private set; }

    public decimal PrixUnitaireTTC { get; private set; }

    public decimal TvaEur { get; private set; }

    public uint TvaPourcentage { get; private set; }
    
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
