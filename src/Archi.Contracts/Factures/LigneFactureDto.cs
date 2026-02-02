using Archi.Contracts.Produits;

namespace Archi.Contracts.Factures;

public class LigneFactureDto
{
    public FactureIdDto FactureId { get; set; } = new ();
    public ProduitIdDto ProduitId { get; set; } = new ();
    public uint Quantite { get; set; }
    public string NomProduit { get; set; } = string.Empty;
    public decimal PrixUnitaireHT { get; set; }
    public decimal Tva { get; set; }
    public uint TvaPourcentage { get; set; }
    public bool EstVerrouille { get; set; }
}