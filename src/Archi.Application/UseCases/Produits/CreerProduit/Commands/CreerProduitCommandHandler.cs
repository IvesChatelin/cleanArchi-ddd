using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Produits;
using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Models;

namespace Archi.Application.UseCases.Produits.CreerProduit.Commands;

public sealed class CreerProduitCommandHandler : ICommandHandler<CreerProduitCommand, ProduitDto>
{
    private readonly IProduitWriteRepository _produitWriteRepository;

    public CreerProduitCommandHandler(IProduitWriteRepository produitWriteRepository)
    {
        _produitWriteRepository = produitWriteRepository;
    }
    
    public async Task<Result<ProduitDto>> Handle(CreerProduitCommand command, CancellationToken cancellationToken)
    {
        var prix = new PrixProduit(command.Produit.PrixUnitaire.PrixUnitaireHt, command.Produit.PrixUnitaire.TvaEur);
        var produit = Produit.Creer(command.Produit.Nom, prix, command.Produit.StockDisponible);

        if (produit.IsFailure)
            return Result<ProduitDto>.Failure(produit.Error);

        var addResult = await _produitWriteRepository.AddAsync(produit.Value!, cancellationToken);

        if (addResult.IsFailure)
            return Result<ProduitDto>.Failure(addResult.Error);

        var produitDto = new ProduitDto()
        {
            Id = new ProduitIdDto() { Value = produit.Value!.Id.Value },
            Nom = produit.Value.Nom,
            PrixUnitaire = new PrixProduitDto()
            {
                PrixUnitaireHt = produit.Value.PrixUnitaire.PrixUnitaireHt,
                TvaEur = produit.Value.PrixUnitaire.TvaEur,
                TvaPourcentage = produit.Value.PrixUnitaire.TvaPourcentage,
                PrixUnitaireTTC = produit.Value.PrixUnitaire.PrixUnitaireTTC
            },
            IsDisponible = produit.Value.IsDisponible,
            StockDisponible = produit.Value.StockDisponible
        };

        return Result<ProduitDto>.Success(produitDto);
    }
}