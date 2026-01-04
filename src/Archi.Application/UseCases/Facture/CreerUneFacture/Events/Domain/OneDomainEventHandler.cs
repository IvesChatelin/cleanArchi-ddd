using Archi.Application.Common.Abstractions.Events;
using Archi.Domain.Common.Models;

namespace Archi.Application.UseCases.Facture.CreerUneFacture.Events.Domain;

public class OneDomainEventHandler : IDomainEventHandler<DomainEvent>
{
    public Task Handle(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
