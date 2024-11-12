namespace NetMicroserviceTemplate.Domain.Entities;

public interface IEntity : IEntity<Guid> { }

public interface IEntity<TKey>
{
    TKey Id { get; }
}
