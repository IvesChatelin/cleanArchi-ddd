using Archi.Domain.Common.Models;
using Archi.Domain.FactureAggregate.Entities;
using Archi.Domain.FactureAggregate.ValueObjects;
using Archi.Domain.ProduitAggregate;

namespace Archi.Domain.FactureAggregate;

public class Facture : AggregateRoot<FactureId>
{
    private readonly List<LigneFacture> _lignes = [];
    public IReadOnlyCollection<LigneFacture> Lignes => _lignes.AsReadOnly();
    public bool EstValidee { get; private set; }
    public DateTime DateCreation { get; private set; }

    private Facture(FactureId id) : base(id)
    {
        EstValidee = false;
        DateCreation = DateTime.UtcNow;
    }

    public static Facture Creer()
    {
        return new Facture(FactureId.Creer());
    }

    public Result<PrixFacture> CalculerTotal()
    {
        var totalApayer = _lignes.Sum(l => l.CalculerTotalPrixUnitaireHt().Value) + _lignes.Sum(l => l.CalculerTotalTvaEur().Value);
        var totalTvaEur = _lignes.Sum(l => l.CalculerTotalTvaEur().Value);
        var prix = new PrixFacture(totalApayer, totalTvaEur);
        return Result<PrixFacture>.Success(prix);
    }

    public Result AjouterUneLigne(Produit produit, uint quantite)
    {
        var ligne = LigneFacture.CreerDepuisProduit(produit, quantite);
        _lignes.Add(ligne.Value!);
        return Result.Success();
    }

    public Result Valider()
    {
        if (!_lignes.Any())
            return Result.Failure(FactureErrors.Vide);

        EstValidee = true;
        return Result.Success();
    }
}
