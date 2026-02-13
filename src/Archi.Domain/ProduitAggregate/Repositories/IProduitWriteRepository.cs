using Archi.SharedKernel.Models;

namespace Archi.Domain.ProduitAggregate.Repositories;

public interface IProduitWriteRepository
{
    Task<Result> AddAsync(Produit entity, CancellationToken ct = default);
    Task<Result> DeleteAsync(Produit entity, CancellationToken ct = default);
    Task<Result> UpdateAsync(Produit entity, CancellationToken ct = default);
    
}