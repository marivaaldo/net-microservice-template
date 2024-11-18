namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

public class EntityRepository<T>(IApplicationContext context) : EntityRepository<Guid, T>(context) where T : class, IEntity
{
}

public class EntityRepository<TKey, T>(IApplicationContext context) : IEntityRepository<TKey, T> where T : class, IEntity<TKey>
{
    protected readonly IApplicationContext _context = context;
    public virtual IUnitOfWork UnitOfWork => _context;

    public virtual void Delete(TKey entity)
        => DeleteAsync(entity).Wait();

    public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await FindByIdAsync(id, cancellationToken);
        if (entity == null)
            return;
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual bool Exists(TKey id)
    {
        var task = ExistsAsync(id);
        task.Wait();
        return task.Result;
    }

    public virtual async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().AnyAsync(x => x.Id.Equals(id), cancellationToken);

    public virtual IEnumerable<T> FindAll()
    {
        var task = FindAllAsync();
        task.Wait();
        return task.Result;
    }

    public virtual async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<T>().ToListAsync(cancellationToken);

    public virtual T FindById(TKey id)
    {
        var task = FindByIdAsync(id);
        task.Wait();
        return task.Result;
    }

    public virtual async Task<T> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync([id], cancellationToken);

    public virtual void Insert(T entity)
        => InsertAync(entity).Wait();

    public virtual async Task InsertAync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual void Update(T entity)
        => UpdateAsync(entity).Wait();

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual void Dispose() { }
}
