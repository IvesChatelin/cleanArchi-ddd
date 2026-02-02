using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Repositories;

public interface IGenericReadRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<Result<TEntity>> GetById(CancellationToken ct = default);
}
