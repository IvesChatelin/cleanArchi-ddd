using Archi.SharedKernel.Events;

namespace Archi.Domain.ProduitAggregate.Events;

public sealed record ProduitDeactiveDomainEvent(Produit Produit) : IDomainEvent;