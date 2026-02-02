namespace Archi.SharedKernel.Helpers;

public interface IRehydratable
{
    // Rehydrate à partir d'un DTO ou d'un état stocké
    public virtual void Rehydrate(object state) {}

}
