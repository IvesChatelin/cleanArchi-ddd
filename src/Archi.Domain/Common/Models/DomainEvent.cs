using Archi.Domain.Common.Abstractions;

namespace Archi.Domain.Common.Models;

public class DomainEvent : IDomainEvent
{
    public Guid Id { get; init; }

    public DomainEvent(Guid id) => Id = id; 
}