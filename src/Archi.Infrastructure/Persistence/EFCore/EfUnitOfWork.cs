using System.Data;
using Archi.SharedKernel.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Archi.Infrastructure.Persistence.EFCore;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public EfUnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction != null)
            return;

        _transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction == null)
            return;
        
        await _context.SaveChangesAsync(ct);
        await _transaction.CommitAsync(ct);
    }

    public async Task RollBackTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction == null)
            return;
        
        await _transaction.RollbackAsync(ct);
        await DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction == null)
            return;
        
        await _transaction.DisposeAsync();
        _transaction = null;
        await _context.DisposeAsync();
    }
}
