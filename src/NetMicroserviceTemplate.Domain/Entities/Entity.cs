namespace NetMicroserviceTemplate.Domain.Entities;

public abstract class Entity : Entity<Guid>, IEntity { }

public abstract class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; protected set; }
}
