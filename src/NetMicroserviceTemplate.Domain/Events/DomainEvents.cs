using Microsoft.Extensions.DependencyInjection;

namespace NetMicroserviceTemplate.Domain.Events;

public static class DomainEvents
{
    public static IServiceProvider ServiceProvider { get; set; }

    public static void Raise<T>(T domainEvent) where T : DomainEvent
    {
        if (ServiceProvider == null)
            throw new InvalidOperationException("ServiceProvider not configured");

        ArgumentNullException.ThrowIfNull(domainEvent);

        var handlers = ServiceProvider.GetServices<IDomainEventHandler<T>>();

        foreach (var handler in handlers)
            handler.Handle(domainEvent);
    }
}
