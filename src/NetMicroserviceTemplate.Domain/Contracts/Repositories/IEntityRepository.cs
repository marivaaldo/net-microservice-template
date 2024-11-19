using DataQI.Commons.Query;
using NetMicroserviceTemplate.Domain.Common;
using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Contracts.Repositories;

public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity> where TEntity : class, IEntity { }

public interface IEntityRepository<TKey, TEntity> : DataQI.Commons.Repository.ICrudRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }
    PagedList<TEntity> GetPaged(int pageNumber, int pageSize, Func<ICriteria, ICriteria> criteriaBuilder = null);
    Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, Func<ICriteria, ICriteria> criteriaBuilder = null, CancellationToken cancellationToken = default);
}
