namespace Archi.Contracts.Produits;

public class ProduitDto
{
    public ProduitIdDto Id { get; set; } = new ();
    public string Nom { get; set; } = string.Empty;
    public PrixProduitDto PrixUnitaire { get; set; } = new ();
    public bool IsDisponible { get; set; } 
    public int StockDisponible { get; set; }
}