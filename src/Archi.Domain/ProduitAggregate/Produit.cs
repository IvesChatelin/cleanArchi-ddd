using Archi.Domain.ProduitAggregate.Events;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Models;

namespace Archi.Domain.ProduitAggregate;

public class Produit : AggregateRoot<ProduitId>
{
    public string Nom { get; private set; }

    public PrixProduit PrixUnitaire { get; private set; }

    public int StockDisponible { get; private set; }

    public bool IsDisponible { get; private set; }

#pragma warning disable CS8618
    private Produit(){}

    private Produit(ProduitId id, string nom, PrixProduit prix, int stockDisponible) :
        base(id)
    {
        Nom = nom;
        PrixUnitaire = prix;
        StockDisponible = stockDisponible;
        IsDisponible = StockDisponible > 0;
    }

    public static Result<Produit> Creer(string nom, PrixProduit prix, int stockDisponible)
    {
        if(nom is null)
            return Result<Produit>.Failure(ProduitErrors.NullName);

        if(prix is null)
            return Result<Produit>.Failure(ProduitErrors.NullPrice);

        if(prix.PrixUnitaireTTC <= 0)
            return Result<Produit>.Failure(ProduitErrors.InvalidPriceUnitaireTTC);
        
        var produit = new Produit(ProduitId.Creer(), nom, prix, stockDisponible);
        produit.RaiseDomainEvent(new ProduitCreeDomainEvent(produit));
        return Result<Produit>.Success(produit);
    }

    public Result Deactiver()
    {
        IsDisponible = false;
        RaiseDomainEvent(new ProduitDeactiveDomainEvent(this));
        return Result.Success();
    }

    public Result ModifierLePrix(PrixProduit nouveauPrix)
    {
        PrixUnitaire = nouveauPrix;
        RaiseDomainEvent(new ProduitModifieDomainEvent(this));
        return Result.Success();
    }
}
