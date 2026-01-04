using Archi.Application.Common.Abstractions.Queries;
using Archi.Contracts.Facture.Dtos;

namespace Archi.Application.UseCases.Facture.CreerUneFacture.Queries;

public sealed record GetFactureByIdQuery(int Id) : IQuery<FactureDto>;
