using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Repositories;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories.Produits;

public class ProduitReadRepository : GenericReadRepository<Produit, ProduitId>, IProduitReadRepository
{
    public ProduitReadRepository(AppDbContext context) : base(context)
    {
    }
}