using Archi.Application.Common.Abstractions.Commands;
using Archi.Application.UseCases.Produits.CreerProduit.Commands;
using Archi.Contracts.Produits;
using Archi.Presentation.Extensions;

namespace Archi.Presentation.EndPoints.ProduitsEndpoints.v1;

public class CreateProduitEndpoint : IEndpoint
{
    public int Version => 1;
    public string Group => Tags.Produits;

    public sealed record CreateProduitRequest(ProduitDto Produit);
    
    public void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPost("/", CreateProduit)
         .WithTags(Tags.Produits)
         .Produces<ProduitDto>(StatusCodes.Status201Created);
    }

    /// <summary>
    /// Créer un produit
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<IResult> CreateProduit(CreateProduitRequest request, 
        ICommandHandler<CreerProduitCommand, ProduitDto> handler, 
        CancellationToken cancellationToken)
    {
        var command = new CreerProduitCommand(request.Produit);
            var result = await handler.Handle(command, cancellationToken);
            return result.Match(
                produitDto => TypedResults.Created($"/produits/{produitDto.Id}", produitDto),
                errors => ResultExtension.Problem(errors));
    }
}