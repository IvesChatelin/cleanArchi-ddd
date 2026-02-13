using System.Text.Json.Serialization;
using Archi.Infrastructure.Persistence.Configurations;
using Archi.Infrastructure.Persistence.EFCore;
using Archi.SharedKernel.UnitOfWork;
using Microsoft.AspNetCore.Http.Json;
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
        services.AddRepositories();
        services.AddJsonOptionsOpenAPI();
        return services;
    }

    public static IServiceCollection AddOptions(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Configure<PostgresOptions> need Microsoft.Extensions.Options.ConfigurationExtensions package
        services.AddOptions<PostgresOptions>()
            .Bind(configuration.GetSection(PostgresOptions.SectionName))
            .ValidateDataAnnotations()
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

    /// <summary>
    /// Permet d'afficher les enums en string
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJsonOptionsOpenAPI(this IServiceCollection services)
    {
        // need Microsoft.AspNetCore.OpenApi package
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }

}
