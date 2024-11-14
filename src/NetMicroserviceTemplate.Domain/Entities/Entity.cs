using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Domain.Entities;

public abstract class Entity : Entity<Guid>, IEntity { }

public abstract class Entity<TKey> : IEntity<TKey>
{
    protected readonly List<IDomainEvent> _domainEvents = [];

    public TKey Id { get; protected set; }

    public IReadOnlyList<IDomainEvent> ConsumeDomainEvents()
    {
        var events = _domainEvents.AsReadOnly().ToList();
        _domainEvents.Clear();
        return events;
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (_domainEvents.Any(x => x.Id == domainEvent.Id))
            return;
        _domainEvents.Add(domainEvent);
    }
}
