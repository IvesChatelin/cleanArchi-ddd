using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Factures;
using Archi.Contracts.Produits;

namespace Archi.Application.UseCases.Factures.CreerFacture.Commands;

public sealed record LigneFactureRequest(
    FactureIdDto FactureId,
    ProduitIdDto ProduitId,
    uint Quantite
);

public sealed record CreerFactureCommand(List<LigneFactureRequest> LigneFacture): ICommand<FactureDto>;