using Archi.Application.Common.Abstractions.Commands;
using Archi.SharedKernel.Models;

namespace Archi.Application.UseCases.Factures.CreerUneFacture.Commands;

public sealed class CreerUneFactureCommandHandler : ICommandHandler<CreerUneFactureCommand>
{
    public Task<Result> Handle(CreerUneFactureCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
