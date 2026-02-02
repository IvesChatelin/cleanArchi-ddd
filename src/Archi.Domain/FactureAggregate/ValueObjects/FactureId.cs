using Archi.SharedKernel.Models;

namespace Archi.Domain.FactureAggregate.ValueObjects;

public class FactureId : ValueObject
{
    public Guid Value { get; private set; }

    private FactureId (Guid value)
    {
        Value = value;
    }

    public static FactureId Creer()
    {
        return new FactureId(Guid.NewGuid());
    }

    public static FactureId CreerWithValue(Guid value)
    {
        return new FactureId(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
