namespace NetMicroserviceTemplate.Domain.Events;

public interface IDomainEventHandler<in T> where T : DomainEvent
{
    void Handle(T domainEvent);
}
