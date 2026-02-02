namespace Archi.SharedKernel.Models;

public abstract class AggregateRoot<T>(T id) : Entity<T>(id)
    where T : notnull
{
}
