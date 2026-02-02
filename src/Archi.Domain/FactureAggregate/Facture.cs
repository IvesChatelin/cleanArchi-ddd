using Archi.Domain.FactureAggregate.Entities;
using Archi.Domain.FactureAggregate.Enums;
using Archi.Domain.FactureAggregate.Events;
using Archi.Domain.FactureAggregate.ValueObjects;
using Archi.Domain.ProduitAggregate;
using Archi.SharedKernel.Models;

namespace Archi.Domain.FactureAggregate;

public class Facture : AggregateRoot<FactureId>
{
    private readonly List<LigneFacture> _lignes = [];
    public IReadOnlyList<LigneFacture> Lignes => _lignes.AsReadOnly();
    public DateTime DateCreation { get; private set; }
    public PrixFacture PrixTotal { get; private set; }
    public StatutFacture Statut { get; private set; }

    private Facture(FactureId id) : base(id)
    {
        DateCreation = DateTime.UtcNow;
        PrixTotal = new PrixFacture(0, 0);
        Statut = StatutFacture.Brouillon;
    }

    public static Result<Facture> Creer()
    {
        var facture = new Facture(FactureId.Creer());
        facture.RaiseDomainEvent(new FactureCreeeDomainEvent(facture));
        return Result<Facture>.Success(facture);
    }

    public Result<PrixFacture> CalculerTotal()
    {
        var totalApayer = Lignes.Sum(l => l.CalculerTotalPrixUnitaireHt().Value) + Lignes.Sum(l => l.CalculerTotalTvaEur().Value);
        var totalTvaEur = Lignes.Sum(l => l.CalculerTotalTvaEur().Value);
        var prix = new PrixFacture(totalApayer, totalTvaEur);
        return Result<PrixFacture>.Success(prix);
    }

    public Result AjouterUneLigne(Produit produit, uint quantite)
    {
        if (EstModifiable().IsSuccess)
        {
            return Result.Failure(FactureErrors.NonModifiable);
        }
        var ligne = LigneFacture.CreerDepuisProduit(produit, quantite, Id);
        _lignes.Add(ligne.Value!);
        return Result.Success();
    }
    public Result ModiferStatut(StatutFacture nouveauStatut)
    {
        Statut = nouveauStatut;
        return Result.Success();
    }

    public Result<PrixFacture> RecalculerPrixTotal()
    {
        PrixTotal = CalculerTotal().Value!;
        return Result<PrixFacture>.Success(PrixTotal);
    }

    public Result EstModifiable()
    {
        if (Statut != StatutFacture.Brouillon)
        {
            return Result.Failure(FactureErrors.NonModifiable);
        }

        return Result.Success();
    }

}
