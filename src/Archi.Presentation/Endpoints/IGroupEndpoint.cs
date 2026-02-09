namespace Archi.Presentation.EndPoints;

public interface IGroupEndpoint
{
    int Version { get; }
    string Tag { get; }
    RouteGroupBuilder MapGroup(IEndpointRouteBuilder app);
}