using Archi.Application.Common.Abstractions.Queries;
using Archi.Application.UseCases.Produits.GetProduit.Queries;
using Archi.Contracts.Produits;
using Archi.Domain.ProduitAggregate;
using Archi.Presentation.Extensions;

namespace Archi.Presentation.EndPoints.ProduitsEndpoints.v1;

public class GetAllProduitEndpoint : IEndpoint
{
    public int Version => 1;

    public string Group => Tags.Produits;

    public void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapGet("/", GetProduits)
            .WithTags(Tags.Produits)
            .Produces<List<ProduitDto>>(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Lister tous les produits
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<IResult> GetProduits (IQueryHandler<GetAllProduitQuery, IEnumerable<Produit>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetAllProduitQuery(), cancellationToken);

        var produits = new List<ProduitDto> ();

        foreach(var produit in result.Value!)
        {
            
            produits.AddRange(new ProduitDto()
            {
                Id = new ProduitIdDto (){Value = produit.Id.Value},
                Nom = produit.Nom,
                PrixUnitaire = new PrixProduitDto()
                {
                    TvaEur = produit.PrixUnitaire.TvaEur,
                    TvaPourcentage = produit.PrixUnitaire.TvaPourcentage,
                    PrixUnitaireHt = produit.PrixUnitaire.PrixUnitaireHt,
                    PrixUnitaireTTC = produit.PrixUnitaire.PrixUnitaireTTC

                },
                IsDisponible = produit.IsDisponible,
                StockDisponible = produit.StockDisponible
            });
        }

        return result.Match(
            produitsDto => TypedResults.Ok(produits),
            errors => ResultExtension.Problem(errors));
    } 
}