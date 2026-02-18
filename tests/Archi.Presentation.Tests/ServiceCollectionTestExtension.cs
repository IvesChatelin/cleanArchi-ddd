using Archi.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Archi.Presentation.Tests;

internal static class ServiceCollectionTestExtension
{
    internal static IServiceCollection ReplaceDbOptions(this IServiceCollection services, PostgreSqlContainer postgreSqlContainer)
    {
        var serviceDescriptor = services
            .SingleOrDefault(s => s.ServiceType == typeof(IConfigureOptions<PostgresOptions>));

        if (serviceDescriptor is not null)
        {
            services.Remove(serviceDescriptor);
        }

        var builder = new NpgsqlConnectionStringBuilder(postgreSqlContainer.GetConnectionString());

        services.Configure<PostgresOptions>(options =>
        {
            options.Database = builder.Database!;
            options.Host = builder.Host!;
            options.Password = builder.Password!;
            options.Username = builder.Username!;
            options.Port = builder.Port!;
        });

        return services;
    }

}