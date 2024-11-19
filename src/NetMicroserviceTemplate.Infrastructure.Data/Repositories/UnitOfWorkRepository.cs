using DataQI.Commons.Query;
using DataQI.EntityFrameworkCore.Query.Support;
using NetMicroserviceTemplate.Domain.Common;
using System.Linq.Dynamic.Core;

namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

public class UnitOfWorkRepository<TEntity>(ApplicationContext context) : UnitOfWorkRepository<Guid, TEntity>(context) where TEntity : class, IEntity<Guid> { }

public abstract class UnitOfWorkRepository<TKey, TEntity>(ApplicationContext context)
    : DataQI.EntityFrameworkCore.Repository.Support.EntityRepository<TEntity, TKey>(context), IEntityRepository<TKey, TEntity>
    where TEntity : class, IEntity<TKey>
{
    public virtual IUnitOfWork UnitOfWork => (IUnitOfWork)base.context;

    public PagedList<TEntity> GetPaged(int pageNumber, int pageSize, Func<ICriteria, ICriteria> criteriaBuilder = null)
    {
        var task = GetPagedAsync(pageNumber, pageSize, criteriaBuilder);
        task.Wait();
        return task.Result;
    }

    public async Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, Func<ICriteria, ICriteria> criteriaBuilder = null, CancellationToken cancellationToken = default)
    {
        var query = base.context
            .Set<TEntity>()
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        if (criteriaBuilder != null)
        {
            var criteria = new EntityCriteria();
            criteriaBuilder(criteria);
            var entityCommand = criteria.BuildCommand();
            query = query.Where(entityCommand.Command, entityCommand.Values);
        }

        var totalItems = query.Count();
        var items = await query.ToListAsync(cancellationToken: cancellationToken);
        return new PagedList<TEntity>(items, totalItems, pageNumber, pageSize);
    }
}
