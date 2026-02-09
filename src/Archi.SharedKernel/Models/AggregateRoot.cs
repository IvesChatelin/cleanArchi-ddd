namespace Archi.SharedKernel.Models;

public abstract class AggregateRoot<T> : Entity<T>
    where T : notnull
{
    protected AggregateRoot(){}
    protected AggregateRoot(T id) : base(id){}
}
