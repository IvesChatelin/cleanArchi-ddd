using Archi.Application.Common.Abstractions.Commands;

namespace Archi.Application.UseCases.Factures.CreerUneFacture.Commands;

public sealed record CreerUneFactureCommand(Guid Id): ICommand;