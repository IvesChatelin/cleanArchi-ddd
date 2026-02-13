using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Produits;
using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Models;
using Mapster;

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
        var prix = new PrixProduit(command.PrixUnitaire.PrixUnitaireHt, command.PrixUnitaire.TvaEur);
        var factoryResult = Produit.Creer(command.Nom, prix, command.StockDisponible);

        if (factoryResult.IsFailure)
            return Result<ProduitDto>.Failure(factoryResult.Error);

        var produit = factoryResult.Value!;

        _ = await _produitWriteRepository.AddAsync(produit, cancellationToken);

        var produitDto = produit.Adapt<ProduitDto>();

        return Result<ProduitDto>.Success(produitDto);
    }
}