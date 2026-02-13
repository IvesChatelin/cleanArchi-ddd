using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Factures;
using Archi.Domain.FactureAggregate;
using Archi.Domain.FactureAggregate.Repositories;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Models;
using Mapster;

namespace Archi.Application.UseCases.Factures.CreerFacture.Commands;

public sealed class CreerFactureCommandHandler : ICommandHandler<CreerFactureCommand, FactureDto>
{
    private readonly IProduitReadRepository _produitReadRepository;
    private readonly IFactureWriteRepository _factureWriteRepository;

    public CreerFactureCommandHandler(IProduitReadRepository produitReadRepository, IFactureWriteRepository factureWriteRepository)
    {
        _produitReadRepository = produitReadRepository;
        _factureWriteRepository = factureWriteRepository;
    }

    public async Task<Result<FactureDto>> Handle(CreerFactureCommand command, CancellationToken cancellationToken)
    {
        var factoryResult = Facture.Creer();
        var facture = factoryResult.Value!;

        foreach (var line in command.LigneFacture)
        {
            var produitResult = await _produitReadRepository.GetByIdAsync(ProduitId.CreerWithValue(line.ProduitId.Value), cancellationToken);

            if (produitResult.IsFailure)
            {
                return Result<FactureDto>.Failure(produitResult.Error);
            }

            var addLineResult = facture.AjouterUneLigne(produitResult.Value!, line.Quantite);

            if (addLineResult.IsFailure)
                Result<FactureDto>.Failure(addLineResult.Error);
        }

        _ = await _factureWriteRepository.AddAsync(facture, cancellationToken);

        var factureDto = facture.Adapt<FactureDto>();

        return Result<FactureDto>.Success(factureDto);

    }
}
