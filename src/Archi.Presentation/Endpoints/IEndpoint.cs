namespace Archi.Presentation.EndPoints;

public interface IEndpoint
{
    int Version { get; }
    string Group { get; }
    void MapEndpoint(RouteGroupBuilder group);
}