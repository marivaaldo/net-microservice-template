using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Contracts.Repositories;

public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity> where TEntity : class, IEntity { }

public interface IEntityRepository<TKey, TEntity> : IDisposable where TEntity : IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    void Delete(TKey entity);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    bool Exists(TKey id);
    Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);

    IEnumerable<TEntity> FindAll();
    Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);

    TEntity FindById(TKey id);
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

    void Insert(TEntity entity);
    Task InsertAync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
