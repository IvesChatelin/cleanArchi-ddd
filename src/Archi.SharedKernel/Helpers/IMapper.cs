namespace Archi.SharedKernel.Helpers;

public interface IMapper<in TSource, out TDestination>
{
    TDestination Map(TSource source);
}
