using Microsoft.EntityFrameworkCore;
using MiniERP.Domain.Entities;

namespace MiniERP.Infrastructure.Data;

public class HuissierDbContext : DbContext
{
    public HuissierDbContext(DbContextOptions<HuissierDbContext> options) : base(options)
    {
    }

    public DbSet<Dossier> Dossiers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration de l'entité Dossier
        modelBuilder.Entity<Dossier>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.NomDebiteur)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(d => d.Montant)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(d => d.DateAudience)
                .IsRequired();

            entity.Property(d => d.Statut)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(d => d.DateCreation)
                .IsRequired();

            entity.Property(d => d.Notes)
                .HasMaxLength(1000);

            // Index pour améliorer les performances
            entity.HasIndex(d => d.Statut)
                .HasDatabaseName("IX_Dossiers_Statut");

            entity.HasIndex(d => d.DateAudience)
                .HasDatabaseName("IX_Dossiers_DateAudience");
        });

        // Données de test (seed data) - on garde les mêmes pour compatibilité
        modelBuilder.Entity<Dossier>().HasData(
            new Dossier
            {
                Id = 1,
                NomDebiteur = "Martin Dubois",
                Montant = 1500.00m,
                DateAudience = new DateTime(2024, 12, 15),
                Statut = Domain.Enums.StatutDossier.Ouvert,
                DateCreation = new DateTime(2024, 9, 4, 10, 0, 0),
                Notes = "Premier dossier de test"
            },
            new Dossier
            {
                Id = 2,
                NomDebiteur = "Sophie Lemoine",
                Montant = 2300.50m,
                DateAudience = new DateTime(2024, 11, 20),
                Statut = Domain.Enums.StatutDossier.EnCours,
                DateCreation = new DateTime(2024, 8, 28, 9, 30, 0),
                Notes = "Dossier en cours de traitement"
            }
        );
    }
}