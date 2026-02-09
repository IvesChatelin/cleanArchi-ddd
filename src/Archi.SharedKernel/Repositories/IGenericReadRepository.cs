using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Repositories;

public interface IGenericReadRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<Result<TEntity>> GetByIdAsync(TId id, CancellationToken ct = default);

    Task<Result<IEnumerable<TEntity>>> GetAllAsync(CancellationToken ct = default);
}
