using Archi.Application.Common.Abstractions.Queries;
using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.SharedKernel.Models;

namespace Archi.Application.UseCases.Produits.GetProduit.Queries;

public class GetAllProduitQueryHandler : IQueryHandler<GetAllProduitQuery, IEnumerable<Produit>>
{
    private readonly IProduitReadRepository _produitReadRepository;

    public GetAllProduitQueryHandler(IProduitReadRepository produitReadRepository)
    {
        _produitReadRepository = produitReadRepository;
    }

    public async Task<Result<IEnumerable<Produit>>> Handle(GetAllProduitQuery query, CancellationToken cancellationToken)
    {
        return await _produitReadRepository.GetAllAsync(cancellationToken);
    }
}