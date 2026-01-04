using Archi.Domain.Common.Models;

namespace Archi.Domain.ProduitAggregate.ValueObjects;

public sealed class ProduitId : ValueObject
{
    public Guid Value { get; private set; }

    private ProduitId(Guid value)
    {
        Value = value;
    }

    public static ProduitId Creer()
    {
        return new ProduitId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
