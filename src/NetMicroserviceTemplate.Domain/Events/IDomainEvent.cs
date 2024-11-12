namespace NetMicroserviceTemplate.Domain.Events;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}
