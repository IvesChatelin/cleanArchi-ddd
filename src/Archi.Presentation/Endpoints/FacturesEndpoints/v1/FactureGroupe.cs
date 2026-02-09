namespace Archi.Presentation.EndPoints.FacturesEndPoints.v1;

public class FactureGroupe : IGroupEndpoint
{
    public int Version => 1;

    public string Tag => Tags.Factures;

    public RouteGroupBuilder MapGroup(IEndpointRouteBuilder app)
    {
        return app.MapGroup($"api/v{Version}/Factures")
            .WithTags(Tag);
    }
}