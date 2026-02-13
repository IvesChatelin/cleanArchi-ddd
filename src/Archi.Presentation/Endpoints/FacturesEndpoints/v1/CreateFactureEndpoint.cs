using Archi.Application.Common.Abstractions.Commands;
using Archi.Application.UseCases.Factures.CreerFacture.Commands;
using Archi.Contracts.Factures;
using Archi.Presentation.Extensions;

namespace Archi.Presentation.EndPoints.FacturesEndPoints.v1;

public class CreateFactureEndpoint : IEndpoint
{
    public int Version => 1;

    public string Group => Tags.Factures;

    public sealed record CreateFactureRequest(List<LigneFactureRequest> Lignes);

    public void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPost("/", CreateFacture)
        .WithTags(Tags.Factures)
        .Produces<FactureDto>(StatusCodes.Status201Created);
    }

    /// <summary>
    /// Créer une facture
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<IResult> CreateFacture(CreateFactureRequest request, ICommandHandler<CreerFactureCommand, FactureDto> handler, CancellationToken cancellationToken)
    {
        var command = new CreerFactureCommand(request.Lignes);

        var result =  await handler.Handle(command, cancellationToken);

        return result.Match(
            onSuccess: facture => Results.Created($"/factures/{facture.Id}", facture),
            onFailure: error => ResultExtension.Problem(error)
        );
    }
}