using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.API.Extensions;

public static class DomainServicesExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
        => services
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddEventHandlers();

    private static IServiceCollection AddEventHandlers(this IServiceCollection services)
        => services.AddImplementationsOf(typeof(IDomainEventHandler));
}
