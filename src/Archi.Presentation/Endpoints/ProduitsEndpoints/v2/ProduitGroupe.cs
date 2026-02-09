namespace Archi.Presentation.EndPoints.ProduitsEndpoints.v2;

public class ProduitGroupe : IGroupEndpoint
{
    public int Version => 2;

    public string Tag => Tags.Produits;

    public RouteGroupBuilder MapGroup(IEndpointRouteBuilder app)
    {
        return app.MapGroup($"api/v{Version}/produits")
            .WithTags(Tag);
    }
}