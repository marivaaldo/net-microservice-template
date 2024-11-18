using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Contracts.Repositories;

public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity> where TEntity : class, IEntity { }

public interface IEntityRepository<TKey, TEntity> : DataQI.Commons.Repository.ICrudRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }
}
