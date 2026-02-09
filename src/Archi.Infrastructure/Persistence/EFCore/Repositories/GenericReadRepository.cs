using Archi.SharedKernel.Errors;
using Archi.SharedKernel.Models;
using Archi.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories;

public class GenericReadRepository<TEntity, TId> : IGenericReadRepository<TEntity, TId> 
    where TEntity : Entity<TId>
{
    protected readonly AppDbContext DbContextext;

    public GenericReadRepository(AppDbContext context)
    {
        DbContextext = context;
    }

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await DbContextext.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(ct);
        return Result<IEnumerable<TEntity>>.Success(entities);
    }

    public async Task<Result<TEntity>> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        var entity = await DbContextext.Set<TEntity>()
            .AsTracking()
            .SingleOrDefaultAsync(e => e.Id!.Equals(id), ct);

        if (entity is null)
        {
            return Result<TEntity>.Failure(EntityError<TEntity, TId>.NotFound(id));
        }

        return Result<TEntity>.Success(entity);
    }
}