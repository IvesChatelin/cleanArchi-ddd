using Archi.SharedKernel.Models;

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

    public static LigneFactureId CreerWithValue(Guid value)
    {
        return new LigneFactureId(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
