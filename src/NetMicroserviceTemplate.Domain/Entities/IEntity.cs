using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Domain.Entities;

public interface IEntity : IEntity<Guid> { }

public interface IEntity<TKey> : IDomainEntity
{
    TKey Id { get; }
}

public interface IDomainEntity
{
    IReadOnlyList<IDomainEvent> ConsumeDomainEvents();
}

