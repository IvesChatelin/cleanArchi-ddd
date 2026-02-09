using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories.Produits;

public class ProduitEntityTypeConfiguration : IEntityTypeConfiguration<Produit>
{
    public void Configure(EntityTypeBuilder<Produit> builder)
    {
        builder.ToTable("Produits");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProduitId.CreerWithValue(value)
            );

        builder.Property(p => p.Nom)
            .IsRequired();

        builder.OwnsOne(p => p.PrixUnitaire, pub =>
        {
            pub.Property(p => p.PrixUnitaireHt).HasColumnName("prix_unitaire_ht");
            pub.Property(p => p.TvaEur).HasColumnName("tva_eur");
            pub.Property(p => p.TvaPourcentage).HasColumnName("tva_pourcentage");
        });

        builder.Property(p => p.StockDisponible)
            .IsRequired()
            .HasColumnName("stock_disponible");

        builder.Property(p => p.IsDisponible)
            .IsRequired()
            .HasColumnName("is_disponible");
    }
}