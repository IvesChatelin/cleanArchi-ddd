using Archi.Domain.FactureAggregate;
using Archi.Domain.FactureAggregate.Entities;
using Archi.Domain.FactureAggregate.ValueObjects;
using Archi.Domain.ProduitAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archi.Infrastructure.Persistence.EFCore.Repositories.Factures;

public class FactureEntityTypeConfiguration : IEntityTypeConfiguration<Facture>
{
    public void Configure(EntityTypeBuilder<Facture> builder)
    {
        ConfigurationTable(builder);
        ConfigureLigneFacture(builder);
        ShadowProperties(builder);
    }

    private void ConfigurationTable(EntityTypeBuilder<Facture> builder)
    {
        builder.ToTable("factures");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => FactureId.CreerWithValue(value)
            );
        // OwnsOne sans configuré les proprietés on a PrixTotal_MontantTotalAPayer
        builder.OwnsOne(f => f.PrixTotal, pfb =>
        {
            pfb.Property(p => p.MontantTotalAPayer).HasColumnName("montant_Total_a_payer");
            pfb.Property(p => p.TotalTvaEur).HasColumnName("total_tva_eur");
            pfb.Property(p => p.MontantTotalHt).HasColumnName("montant_total_ht");
        });
        builder.Property(f => f.DateCreation)
            .HasColumnName("date_creation");

        builder.Metadata.FindNavigation(nameof(Facture.Lignes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

    }

    private void ConfigureLigneFacture(EntityTypeBuilder<Facture> builder)
    {
        builder.OwnsMany(f => f.Lignes, lfb =>
        {
            lfb.ToTable("lignes_factures");
            
            lfb.WithOwner().HasForeignKey("FactureId");

            lfb.HasKey(l => l.Id);

            lfb.Property(l => l.Id)
                .HasColumnName("ligne_facture_id")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => LigneFactureId.CreerWithValue(value)
                );

            lfb.Property(l => l.ProduitId)
                .HasColumnName("produit_id")
                .HasConversion(
                    id => id.Value,
                    value => ProduitId.CreerWithValue(value)
                );

            // use navigation here to configure private field mapping if we have one
            /*lfb.Navigation(l => l.ProduitId)
                .HasField("_produitId")
                .UsePropertyAccessMode(PropertyAccessMode.Field);*/
            
        });
    }

    private void ShadowProperties(EntityTypeBuilder<Facture> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .IsRequired();
        builder.Property<DateTime>("UpdatedDate")
            .IsRequired();
    } 
}
