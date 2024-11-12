namespace NetMicroserviceTemplate.Domain.Events;

public abstract class DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
