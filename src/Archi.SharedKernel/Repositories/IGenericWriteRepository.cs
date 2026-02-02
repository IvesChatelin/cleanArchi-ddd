using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Repositories;

public interface IGenericWriteRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<Result> Save(TEntity entity, CancellationToken ct = default);
}
