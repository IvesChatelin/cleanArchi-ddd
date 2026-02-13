using Archi.Infrastructure.Persistence.Configurations;
using Archi.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Archi.Presentation.Tests;

internal static class ServiceCollectionTestExtension
{
    internal static IServiceCollection RemoveDbContext(this IServiceCollection services, string connectionString)
    {
        var serviceDescriptor = services
            .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<AppDbContext>));

        if (serviceDescriptor is not null)
        {
            services.Remove(serviceDescriptor);
        }

        var builder = new NpgsqlConnectionStringBuilder(connectionString);

        services.Configure<PostgresOptions>(options =>
        {
            options.Database = builder.Database!;
            options.Host = builder.Host!;
            options.Password = builder.Password!;
            options.Username = builder.Username!;
        });

        services.AddDbContext<AppDbContext>();

        return services;
    }
}