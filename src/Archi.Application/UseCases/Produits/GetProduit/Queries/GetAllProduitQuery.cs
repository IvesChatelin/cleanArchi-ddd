using Archi.Application.Common.Abstractions.Queries;
using Archi.Domain.ProduitAggregate;

namespace Archi.Application.UseCases.Produits.GetProduit.Queries;

public sealed record GetAllProduitQuery() : IQuery<IEnumerable<Produit>>;