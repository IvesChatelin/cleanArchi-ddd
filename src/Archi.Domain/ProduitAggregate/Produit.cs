using Archi.Domain.Common.Models;
using Archi.Domain.ProduitAggregate.ValueObjects;

namespace Archi.Domain.ProduitAggregate;

public class Produit : AggregateRoot<ProduitId>
{
    public string Nom { get; private set; }

    public PrixProduit PrixUnitaire { get; private set; }

    public bool IsDisponible { get; private set; } 

    private Produit(ProduitId id, string nom, PrixProduit prix, bool isDisponible) :
        base(id)
    {
        Nom = nom;
        PrixUnitaire = prix;
        IsDisponible = isDisponible;
    }

    public static Result<Produit> Creer(string nom, PrixProduit prix, bool isDisponible)
    {
        if(nom is null)
            return Result<Produit>.Failure(ProduitErrors.NullName);
        
        return Result<Produit>.Success(
            new Produit(ProduitId.Creer(), nom, prix, isDisponible)
        );
    }

    public Result Deactiver()
    {
        IsDisponible = false;
        return Result.Success();
    }

    public Result ModifierLePrix(PrixProduit nouveauPrix)
    {
        PrixUnitaire = nouveauPrix;
        return Result.Success();
    }
}
