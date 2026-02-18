using Archi.Domain.ProduitAggregate.Events;
using Archi.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace Archi.Application.UseCases.Produits.CreerProduit.Events;

public class ProduitCreeDomainEventHandler : IDomainEventHandler<ProduitCreeDomainEvent>
{
    private readonly ILogger<ProduitCreeDomainEventHandler> _logger;

    public ProduitCreeDomainEventHandler(ILogger<ProduitCreeDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProduitCreeDomainEvent notification, CancellationToken cancellationToken)
    {
        // Handle the event (e.g., log it, update read models, etc.)
        _logger.LogInformation("Notificatif : Produit créé : {Nom}", notification.ProduitId.Value);
        return Task.CompletedTask;
    }
}