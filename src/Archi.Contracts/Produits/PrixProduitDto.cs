namespace Archi.Contracts.Produits;

public class PrixProduitDto
{
    public decimal PrixUnitaireHt { get; set; }

    public decimal PrixUnitaireTTC { get; set; }

    public decimal TvaEur { get; set; }

    public uint TvaPourcentage { get; set; }
}