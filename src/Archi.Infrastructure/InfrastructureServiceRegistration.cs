using System.Text.Json;
using System.Text.Json.Serialization;
using Archi.Infrastructure.Persistence.Configurations;
using Archi.Infrastructure.Persistence.EFCore;
using Archi.SharedKernel.Errors;
using Archi.SharedKernel.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Archi.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddOptions(configuration); // add as first service
        services.AddRepositories();
        services.AddJsonOptions();
        return services;
    }

    public static IServiceCollection AddOptions(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // need Microsoft.Extensions.Options.ConfigurationExtensions package
        services.AddOptions<PostgresOptions>()
            .Bind(configuration.GetSection(PostgresOptions.SectionName))
            .ValidateOnStart();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        
        services.AddDbContext<AppDbContext>();

        services.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureServiceRegistration).Assembly)
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }

    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.WriteIndented = true;
            options.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }

}
