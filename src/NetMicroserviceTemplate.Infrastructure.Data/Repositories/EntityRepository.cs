namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

internal class EntityRepository<T>(ApplicationContext context) : EntityRepository<Guid, T>(context) where T : class, IEntity
{
}

internal class EntityRepository<TKey, T>(ApplicationContext context) : IEntityRepository<TKey, T> where T : class, IEntity<TKey>
{
    protected readonly ApplicationContext _context = context;

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await FindByIdAsync(id, cancellationToken);
        if (entity == null)
            return;
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<T>().ToListAsync(cancellationToken);

    public async Task<T> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync([id], cancellationToken);

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Detached;
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
