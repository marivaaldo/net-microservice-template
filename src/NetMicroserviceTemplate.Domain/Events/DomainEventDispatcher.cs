using Microsoft.Extensions.DependencyInjection;

namespace NetMicroserviceTemplate.Domain.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlers = _serviceProvider.GetServices(typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType()));

            foreach (var handler in handlers)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var method = handlerType.GetMethod("HandleAsync");

                if (method != null)
                    await (Task)method.Invoke(handler, [domainEvent, cancellationToken]);
            }
        }
    }
}

