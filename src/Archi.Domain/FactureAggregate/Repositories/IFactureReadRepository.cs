using Archi.Domain.FactureAggregate.ValueObjects;
using Archi.SharedKernel.Models;

namespace Archi.Domain.FactureAggregate.Repositories;

public interface IFactureReadRepository
{
    Task<Result<Facture>> GetByIdAsync(FactureId id, CancellationToken ct = default);

    Task<Result<IEnumerable<Facture>>> GetAllAsync(CancellationToken ct = default);
}
