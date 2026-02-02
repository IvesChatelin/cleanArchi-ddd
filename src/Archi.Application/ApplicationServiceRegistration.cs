using System.Reflection;
using Archi.Application.Common.Abstractions.Commands;
using Archi.Application.Common.Abstractions.Queries;
using Archi.Application.Common.Behaviors;
using Archi.SharedKernel.Events;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Archi.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<IDomainEventDispatcher>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandHandler<>));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}
