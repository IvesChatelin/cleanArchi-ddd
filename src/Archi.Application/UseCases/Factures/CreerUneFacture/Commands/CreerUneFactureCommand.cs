using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Factures;

namespace Archi.Application.UseCases.Factures.CreerUneFacture.Commands;

public sealed record CreerUneFactureCommand(List<LigneFactureDto> Produits): ICommand<FactureDto>;