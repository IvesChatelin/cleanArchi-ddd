using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Factures;
using Archi.SharedKernel.Models;

namespace Archi.Application.UseCases.Factures.CreerUneFacture.Commands;

public sealed class CreerUneFactureCommandHandler : ICommandHandler<CreerUneFactureCommand, FactureDto>
{
    public Task<Result<FactureDto>> Handle(CreerUneFactureCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
