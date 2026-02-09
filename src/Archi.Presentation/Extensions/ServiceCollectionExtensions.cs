using System.Reflection;
using Microsoft.OpenApi;

namespace Archi.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // Register presentation layer services here
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Gestion des factures API",
                Description = "Une API ASP.NET Core pour la gestion des factures.",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddCustomizeProblemDetails(this IServiceCollection services)
    {
        // Register presentation layer services here
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                // Additional customization can be done here
                ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
            };
            // Additional mappings can be added here
        });

        return services;
    }
    
}