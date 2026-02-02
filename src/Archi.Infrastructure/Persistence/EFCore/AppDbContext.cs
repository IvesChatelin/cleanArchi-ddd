using Archi.Domain.FactureAggregate;
using Archi.Domain.ProduitAggregate;
using Archi.Infrastructure.Persistence.Configurations;
using Archi.SharedKernel.Events;
using Archi.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Archi.Infrastructure.Persistence.EFCore;

public class AppDbContext : DbContext
{
    public DbSet<Facture> Students { get; set; }
    public DbSet<Produit> Grades { get; set; }
    private readonly IDomainEventDispatcher _domainEventsDispatcher;
    private readonly PostgresOptions _options;

    public AppDbContext(
        IDomainEventDispatcher domainEventsDispatcher, 
        IOptions<PostgresOptions> options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
        _options = options.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        // need Npgsql.EntityFrameworkCore.PostgreSQL package
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_options.ConnectionString());
    }

    protected new async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        int result = await base.SaveChangesAsync(ct);
        await PublishDomainEventsAsync();
        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = [];

                foreach(var domainEvent in entity.GetDomainEvents())
                {
                    domainEvents.AddRange(domainEvent);
                }

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();
        
        await _domainEventsDispatcher.DispatchAsync(domainEvents);
    } 
}
