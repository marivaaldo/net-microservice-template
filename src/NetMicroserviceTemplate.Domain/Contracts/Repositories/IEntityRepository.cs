using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Contracts.Repositories;

public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity> where TEntity : class, IEntity { }

public interface IEntityRepository<TKey, TEntity> where TEntity : IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }
    Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
