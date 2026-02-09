using Archi.Application.Common.Abstractions.Commands;
using Archi.Application.UseCases.Factures.CreerUneFacture.Commands;
using Archi.Contracts.Factures;
using Archi.Presentation.Extensions;

namespace Archi.Presentation.EndPoints.FacturesEndPoints.v1;

public class CreateFactures : IEndpoint
{
    public int Version => 1;

    public string Group => Tags.Factures;

    public sealed record CreateFactureRequest(List<LigneFactureDto> Produits);

    public void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateFactureRequest request, ICommandHandler<CreerUneFactureCommand, FactureDto> handler, CancellationToken cancellationToken) =>
        {
            var command = new CreerUneFactureCommand(request.Produits);

            var result =  await handler.Handle(command, cancellationToken);

            return result.Match(
                onSuccess: facture => Results.Created($"/factures/{facture.Id}", facture),
                onFailure: error => ResultExtension.Problem(error)
            );
        })
        .WithTags(Tags.Factures);
    }
}