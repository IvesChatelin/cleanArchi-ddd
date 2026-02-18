using System.Text.Json;
using Archi.Infrastructure.Persistence.Configurations;
using Archi.SharedKernel.Events;
using Archi.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Archi.Infrastructure.Persistence.EFCore;

public class AppDbContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    private readonly IDomainEventDispatcher _domainEventsDispatcher;
    private readonly PostgresOptions _options;

    public AppDbContext(
        IDomainEventDispatcher domainEventsDispatcher, 
        IOptionsMonitor<PostgresOptions> options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
        _options = options.CurrentValue;
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

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        await PublishDomainEventsAsync(); // Before because I transform domain event to outbox Message
        int result = await base.SaveChangesAsync(ct);
        //await PublishDomainEventsAsync(); // not distrubuate transaction handler domain event executed imediatly
        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        // only not distribued transaction
        /*var domainEvents = ChangeTracker
            .Entries<IHasDomainEvent>()
            .Select(entry => entry.Entity)
            .SelectMany(aggregate =>
            {
                var events = aggregate.GetDomainEvents().ToList();
                aggregate.ClearDomainEvents();
                return events;
            })
            .ToList();

        await _domainEventsDispatcher.DispatchAsync(domainEvents);*/

        var outboxMessages = ChangeTracker
            .Entries<IHasDomainEvent>()
            .Select(entry => entry.Entity)
            .SelectMany(aggregate =>
            {
                var events = aggregate.GetDomainEvents().ToList();
                aggregate.ClearDomainEvents();
                return events;
            })
            .Select(domainEvent => new OutboxMessage()
            {
                Id = Guid.NewGuid(),
                OccuredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonSerializer.Serialize(domainEvent, domainEvent.GetType())
            })
            .ToList();

        await OutboxMessages.AddRangeAsync(outboxMessages);
        

    } 
}
