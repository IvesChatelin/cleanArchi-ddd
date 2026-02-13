using Archi.SharedKernel.Errors;
using Archi.SharedKernel.Models;
using Archi.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories;

public class GenericWriteRepository<TEntity, TId> : IGenericWriteRepository<TEntity, TId>
    where TEntity : Entity<TId>
{
    protected readonly AppDbContext Dbcontext;

    public GenericWriteRepository(AppDbContext context)
    {
        Dbcontext = context;
    }

    public Task<Result> DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        Dbcontext.Set<TEntity>().Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await Dbcontext.Set<TEntity>().AddAsync(entity, ct);
        return Result.Success();
    }

    public Task<Result> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        Dbcontext.Set<TEntity>().Update(entity);
        return Task.FromResult(Result.Success());
    }
}