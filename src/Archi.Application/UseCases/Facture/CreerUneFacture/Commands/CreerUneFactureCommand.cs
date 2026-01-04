using Archi.Application.Common.Abstractions.Commands;

namespace Archi.Application.UseCases.Facture.CreerUneFacture.Commands;

public sealed record CreerUneFactureCommand(Guid Id): ICommand;