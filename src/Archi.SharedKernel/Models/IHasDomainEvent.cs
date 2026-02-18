using Archi.SharedKernel.Events;

namespace Archi.SharedKernel.Models;

/// <summary>
/// Interface qui sert de type pour les entités pour IoC
/// </summary>
public interface IHasDomainEvent
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
