namespace Archi.SharedKernel.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitTransactionAsync(CancellationToken ct = default);
    Task RollBackTransactionAsync(CancellationToken ct = default);
}
