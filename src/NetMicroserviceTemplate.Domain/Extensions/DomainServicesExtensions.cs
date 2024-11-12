using Microsoft.Extensions.DependencyInjection;
using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Domain.Extensions;

public static class DomainServicesExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
        => services
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddEventHandlers();

    private static IServiceCollection AddEventHandlers(this IServiceCollection services)
        => services.AddImplementationsOf(typeof(IDomainEventHandler));

    public static IServiceCollection AddImplementationsOf<TInterface>(this IServiceCollection services)
        => services.AddImplementationsOf(typeof(TInterface));

    public static IServiceCollection AddImplementationsOf(this IServiceCollection services, Type type)
    {
        if (!type.IsInterface)
            throw new InvalidOperationException($"Type {type.FullName} must be a interface");

        var implementations = type.Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(type))
            .ToList();

        foreach (var implementation in implementations)
        {
            var @interface = implementation.GetInterfaces().First();
            services.AddScoped(@interface, implementation);
        }

        return services;
    }
}
