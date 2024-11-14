namespace NetMicroserviceTemplate.Application.Events;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
    string Name { get; }
}

public abstract class IntegrationEvent : IIntegrationEvent, INotification
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string Name => GetType().Name;
    public override string ToString() => Name;
}
