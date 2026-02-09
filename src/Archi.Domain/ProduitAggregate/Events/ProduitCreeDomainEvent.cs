using Archi.SharedKernel.Events;

namespace Archi.Domain.ProduitAggregate.Events;

public sealed record ProduitCreeDomainEvent(Produit Produit) : IDomainEvent;