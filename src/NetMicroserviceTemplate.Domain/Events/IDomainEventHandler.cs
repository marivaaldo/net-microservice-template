namespace NetMicroserviceTemplate.Domain.Events;

public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler where TDomainEvent : IDomainEvent
{
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

public interface IDomainEventHandler { }
