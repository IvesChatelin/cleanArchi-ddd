namespace Archi.Contracts.Factures;

public class PrixFactureDto
{
    public decimal MontantTotalHt { get; set; }

    public decimal MontantTotalAPayer { get; set; }

    public decimal TotalTvaEur { get; set; }
}