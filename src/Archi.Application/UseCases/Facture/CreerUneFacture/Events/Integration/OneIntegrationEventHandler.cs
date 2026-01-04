using Archi.Application.Common.Abstractions.Events;
using Archi.Domain.Common.Models;

namespace Archi.Application.UseCases.Facture.CreerUneFacture.Events.Integration;

/// <summary>
/// Pour transformer le domain event en intergration event
/// </summary>
public class OneIntegrationEventHandler : IDomainEventHandler<DomainEvent>
{
    public Task Handle(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
