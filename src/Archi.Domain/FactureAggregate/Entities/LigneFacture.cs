using Archi.Domain.FactureAggregate.ValueObjects;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.Domain.ProduitAggregate;
using Archi.SharedKernel.Models;

namespace Archi.Domain.FactureAggregate.Entities;

public class LigneFacture : Entity<LigneFactureId>
{
    public FactureId FactureId { get; private set; }
    public ProduitId ProduitId { get; private set; }
    public uint Quantite { get; private set; }
    public string NomProduit { get; private set; }
    public decimal PrixUnitaireHT { get; private set; }
    public decimal Tva { get; private set; }
    public uint TvaPourcentage { get; private set; }
    public bool EstVerrouille { get; private set; }

    private LigneFacture(
        LigneFactureId id,
        FactureId factureId,
        uint quantite,
        ProduitId produitId,
        string nomProduit,
        decimal prixUnitaireHT,
        decimal tva,
        uint tvaPourcentage) : base(id)
    {
        FactureId = factureId;
        Quantite = quantite;
        ProduitId = produitId;
        NomProduit = nomProduit;
        PrixUnitaireHT = prixUnitaireHT;
        Tva = tva;
        TvaPourcentage = tvaPourcentage;
        EstVerrouille = false;
    }

    public static Result<LigneFacture> CreerDepuisProduit(Produit produit, uint quantite, FactureId factureId)
    {
        var ligneFacture = new LigneFacture(
            LigneFactureId.Creer(),
            factureId,
            quantite,
            produit.Id,
            produit.Nom,
            produit.PrixUnitaire.PrixUnitaireHt,
            produit.PrixUnitaire.TvaEur,
            produit.PrixUnitaire.TvaPourcentage
        );

        return Result<LigneFacture>.Success(ligneFacture);
    }

    public Result<decimal> CalculerTotalPrixUnitaireHt()
    {
        decimal total = Quantite * PrixUnitaireHT;
        return Result<decimal>.Success(total);
    }

    public Result<decimal> CalculerTotalTvaEur()
    {
        decimal total = Quantite * Tva;
        return Result<decimal>.Success(total);
    }

    public Result<uint> CalculerTotalTvaPorcentage()
    {
        uint total = (uint)Math.Ceiling(CalculerTotalTvaEur().Value! / CalculerTotalPrixUnitaireHt().Value! * 100);
        return Result<uint>.Success(total);
    }
}
