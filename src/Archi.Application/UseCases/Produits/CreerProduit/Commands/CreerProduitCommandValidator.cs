using Archi.Domain.ProduitAggregate;
using FluentValidation;

namespace Archi.Application.UseCases.Produits.CreerProduit.Commands;

public class CreerProduitCommandValidator : AbstractValidator<CreerProduitCommand>
{
    public CreerProduitCommandValidator()
    {
        RuleFor(x => x.Produit).NotNull();
        RuleFor(x => x.Produit.Nom)
            .NotEmpty()
            .WithMessage(ProduitErrors.NullName.Description)
            .WithErrorCode(ProduitErrors.NullName.Code);
        RuleFor(x => x.Produit.PrixUnitaire)
            .NotNull()
            .WithErrorCode(ProduitErrors.NullPrice.Code)
            .WithMessage(ProduitErrors.NullPrice.Description)
            .Must(prix => prix.PrixUnitaireTTC >= 0)
            .WithErrorCode(ProduitErrors.InvalidPriceUnitaireTTC.Code)
            .WithMessage(ProduitErrors.InvalidPriceUnitaireTTC.Description);
    }
}