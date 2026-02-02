using Archi.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Archi.Infrastructure;

public static class InfrastructureServiceRegistration
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddOptions(configuration); // add as first service
        return services;
    }

    public static IServiceCollection AddOptions(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // need Microsoft.Extensions.Options.ConfigurationExtensions package
        services.Configure<PostgresOptions>(configuration.GetSection(PostgresOptions.SectionName));
        return services;
    }

}
