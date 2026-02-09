using Archi.Domain.ProduitAggregate.ValueObjects;
using Archi.SharedKernel.Models;

namespace Archi.Domain.ProduitAggregate.Repositories;

public interface IProduitReadRepository
{
    Task<Result<Produit>> GetByIdAsync(ProduitId id, CancellationToken ct = default);

    Task<Result<IEnumerable<Produit>>> GetAllAsync(CancellationToken ct = default);
}