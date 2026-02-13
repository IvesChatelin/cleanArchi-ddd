CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE public.factures (
    "Id" uuid NOT NULL,
    date_creation timestamp with time zone NOT NULL,
    montant_total_ht numeric NOT NULL,
    "montant_Total_a_payer" numeric NOT NULL,
    total_tva_eur numeric NOT NULL,
    "Statut" integer NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "UpdatedDate" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_factures" PRIMARY KEY ("Id")
);

CREATE TABLE public."Produits" (
    "Id" uuid NOT NULL,
    "Nom" text NOT NULL,
    prix_unitaire_ht numeric NOT NULL,
    "PrixUnitaire_PrixUnitaireTTC" numeric NOT NULL,
    tva_eur numeric NOT NULL,
    tva_pourcentage bigint NOT NULL,
    stock_disponible integer NOT NULL,
    is_disponible boolean NOT NULL,
    CONSTRAINT "PK_Produits" PRIMARY KEY ("Id")
);

CREATE TABLE public.lignes_factures (
    ligne_facture_id uuid NOT NULL,
    "FactureId" uuid NOT NULL,
    produit_id uuid NOT NULL,
    "Quantite" bigint NOT NULL,
    "NomProduit" text NOT NULL,
    "PrixUnitaireHT" numeric NOT NULL,
    "Tva" numeric NOT NULL,
    "TvaPourcentage" bigint NOT NULL,
    "EstVerrouille" boolean NOT NULL,
    CONSTRAINT "PK_lignes_factures" PRIMARY KEY (ligne_facture_id),
    CONSTRAINT "FK_lignes_factures_factures_FactureId" FOREIGN KEY ("FactureId") REFERENCES public.factures ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_lignes_factures_FactureId" ON public.lignes_factures ("FactureId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260206011103_initialCreate', '10.0.3');

COMMIT;

