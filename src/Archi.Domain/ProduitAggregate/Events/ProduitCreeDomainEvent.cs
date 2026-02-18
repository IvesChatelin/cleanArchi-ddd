using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Events;

namespace Archi.Domain.ProduitAggregate.Events;

public sealed record ProduitCreeDomainEvent(ProduitId ProduitId) : IDomainEvent;