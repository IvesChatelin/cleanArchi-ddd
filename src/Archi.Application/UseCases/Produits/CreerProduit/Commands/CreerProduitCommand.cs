using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Produits;

namespace Archi.Application.UseCases.Produits.CreerProduit.Commands;

public sealed record CreerProduitCommand(
    ProduitIdDto Id,
    string Nom,
    PrixProduitDto PrixUnitaire,
    int StockDisponible
) : ICommand<ProduitDto>;