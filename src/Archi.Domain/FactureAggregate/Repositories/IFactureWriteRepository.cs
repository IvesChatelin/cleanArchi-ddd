using Archi.SharedKernel.Models;

namespace Archi.Domain.FactureAggregate.Repositories;

public interface IFactureWriteRepository
{
    Task<Result> AddAsync(Facture entity, CancellationToken ct = default);
    Task<Result> DeleteAsync(Facture entity, CancellationToken ct = default);
    Task<Result> UpdateAsync(Facture entity, CancellationToken ct = default);
}
