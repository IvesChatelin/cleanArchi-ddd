using Archi.Application.Common.Abstractions.Commands;
using Archi.Domain.Common.Models;

namespace Archi.Application.UseCases.Facture.CreerUneFacture.Commands;

public class CreerUneFactureCommandHandler : ICommandHandler<CreerUneFactureCommand>
{
    public Task<Result> Handle(CreerUneFactureCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
