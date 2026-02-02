using Archi.SharedKernel.Events;

namespace Archi.Domain.FactureAggregate.Events;

public record FactureCreeeDomainEvent (Facture Facture) : IDomainEvent;
