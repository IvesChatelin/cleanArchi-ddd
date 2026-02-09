using System.Reflection;
using Archi.Presentation.EndPoints;

namespace Archi.Presentation.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo<IEndpoint>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo<IGroupEndpoint>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        var groups = app.Services.GetRequiredService<IEnumerable<IGroupEndpoint>>();
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        var groupMap = groups.ToDictionary(
            g => (g.Tag, g.Version),
            g => g.MapGroup(app)
        );

        foreach (var endpoint in endpoints)
        {
            if (groupMap.TryGetValue((endpoint.Group, endpoint.Version), out var group))
            {
                endpoint.MapEndpoint(group);
            }
        }

        return app;
    }

    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, string permission)
    {
        return app.RequireAuthorization(permission);
    }
}
