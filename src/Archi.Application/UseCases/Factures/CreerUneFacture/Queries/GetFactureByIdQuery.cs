using Archi.Application.Common.Abstractions.Queries;
using Archi.Contracts.Factures.Dtos;

namespace Archi.Application.UseCases.Factures.CreerUneFacture.Queries;

public sealed record GetFactureByIdQuery(int Id) : IQuery<FactureDto>;
