using Archi.Domain.Common.Models;

namespace Archi.Domain.FactureAggregate.ValueObjects;

public class LigneFactureId : ValueObject
{
    public Guid Value { get; private set; }

    private LigneFactureId(Guid value)
    {
        Value = value;
    }

    public static LigneFactureId Creer()
    {
        return new LigneFactureId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
