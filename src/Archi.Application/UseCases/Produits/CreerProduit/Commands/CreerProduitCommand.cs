using Archi.Application.Common.Abstractions.Commands;
using Archi.Contracts.Produits;

namespace Archi.Application.UseCases.Produits.CreerProduit.Commands;

public sealed record CreerProduitCommand(ProduitDto Produit) : ICommand<ProduitDto>;