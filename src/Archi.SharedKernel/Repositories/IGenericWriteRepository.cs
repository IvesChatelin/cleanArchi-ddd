using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Repositories;

public interface IGenericWriteRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<Result> AddAsync(TEntity entity, CancellationToken ct = default);
    Task<Result> DeleteAsync(TEntity entity, CancellationToken ct = default);
    Task<Result> UpdateAsync(TEntity entity, CancellationToken ct = default);
}
