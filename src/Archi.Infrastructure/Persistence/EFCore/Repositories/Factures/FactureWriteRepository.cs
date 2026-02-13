using Archi.Domain.FactureAggregate;
using Archi.Domain.FactureAggregate.Repositories;
using Archi.Domain.FactureAggregate.ValueObjects;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories.Factures;

public class FactureWriteRepository : GenericWriteRepository<Facture, FactureId>, IFactureWriteRepository
{
    public FactureWriteRepository(AppDbContext context) : base(context)
    {
    }
}