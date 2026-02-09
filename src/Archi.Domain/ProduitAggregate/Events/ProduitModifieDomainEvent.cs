using Archi.SharedKernel.Events;

namespace Archi.Domain.ProduitAggregate.Events;

public sealed record ProduitModifieDomainEvent(Produit Produit) : IDomainEvent;