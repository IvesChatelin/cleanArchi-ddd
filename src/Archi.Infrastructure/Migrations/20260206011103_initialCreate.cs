using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "factures",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_creation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    montant_total_ht = table.Column<decimal>(type: "numeric", nullable: false),
                    montant_Total_a_payer = table.Column<decimal>(type: "numeric", nullable: false),
                    total_tva_eur = table.Column<decimal>(type: "numeric", nullable: false),
                    Statut = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_factures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    prix_unitaire_ht = table.Column<decimal>(type: "numeric", nullable: false),
                    PrixUnitaire_PrixUnitaireTTC = table.Column<decimal>(type: "numeric", nullable: false),
                    tva_eur = table.Column<decimal>(type: "numeric", nullable: false),
                    tva_pourcentage = table.Column<long>(type: "bigint", nullable: false),
                    stock_disponible = table.Column<int>(type: "integer", nullable: false),
                    is_disponible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lignes_factures",
                schema: "public",
                columns: table => new
                {
                    ligne_facture_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FactureId = table.Column<Guid>(type: "uuid", nullable: false),
                    produit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantite = table.Column<long>(type: "bigint", nullable: false),
                    NomProduit = table.Column<string>(type: "text", nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "numeric", nullable: false),
                    Tva = table.Column<decimal>(type: "numeric", nullable: false),
                    TvaPourcentage = table.Column<long>(type: "bigint", nullable: false),
                    EstVerrouille = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lignes_factures", x => x.ligne_facture_id);
                    table.ForeignKey(
                        name: "FK_lignes_factures_factures_FactureId",
                        column: x => x.FactureId,
                        principalSchema: "public",
                        principalTable: "factures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lignes_factures_FactureId",
                schema: "public",
                table: "lignes_factures",
                column: "FactureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lignes_factures",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Produits",
                schema: "public");

            migrationBuilder.DropTable(
                name: "factures",
                schema: "public");
        }
    }
}
