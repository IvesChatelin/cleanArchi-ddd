using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.Domain.ProduitAggregate.ValueObjects;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories.Produits;

public class ProduitWriteRepository : GenericWriteRepository<Produit, ProduitId>, IProduitWriteRepository
{
    public ProduitWriteRepository(AppDbContext context) : base(context)
    {
    }
}