namespace Archi.Presentation.EndPoints.ProduitsEndpoints.v1;

public class ProduitGroupe : IGroupEndpoint
{
    public int Version => 1;

    public string Tag => Tags.Produits;

    public RouteGroupBuilder MapGroup(IEndpointRouteBuilder app)
    {
        return app.MapGroup($"api/v{Version}/produits")
            .WithTags(Tag);
    }
}