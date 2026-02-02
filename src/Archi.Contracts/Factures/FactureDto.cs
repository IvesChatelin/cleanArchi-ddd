namespace Archi.Contracts.Factures;

public class FactureDto
{
    public FactureIdDto Id { get; set; } = new ();
    public List<LigneFactureDto> Lignes { get; set; } = [];
    public DateTime DateCreation { get; set; }
    public PrixFactureDto PrixTotal { get; set; } = new ();
    public StatutFactureDto Statut { get; set; }
}
