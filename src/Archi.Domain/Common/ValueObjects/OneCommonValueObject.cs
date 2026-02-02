using Archi.SharedKernel.Models;

namespace Archi.Domain.Common.ValueObjects;

public class OneCommonValueObject : ValueObject
{
    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
