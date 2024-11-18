namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

public class UnitOfWorkRepository<TEntity>(ApplicationContext context) : UnitOfWorkRepository<Guid, TEntity>(context) where TEntity : class, IEntity<Guid> { }

public abstract class UnitOfWorkRepository<TKey, TEntity>(ApplicationContext context)
    : DataQI.EntityFrameworkCore.Repository.Support.EntityRepository<TEntity, TKey>(context), IEntityRepository<TKey, TEntity>
    where TEntity : class, IEntity<TKey>
{
    public virtual IUnitOfWork UnitOfWork => (IUnitOfWork)base.context;
}
