﻿namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

public class EntityRepository<T>(IApplicationContext context) : EntityRepository<Guid, T>(context) where T : class, IEntity
{
}

public class EntityRepository<TKey, T>(IApplicationContext context) : IEntityRepository<TKey, T> where T : class, IEntity<TKey>
{
    protected readonly IApplicationContext _context = context;
    public virtual IUnitOfWork UnitOfWork => _context;

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await FindByIdAsync(id, cancellationToken);
        if (entity == null)
            return;
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<T>().ToListAsync(cancellationToken);

    public virtual async Task<T> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync([id], cancellationToken);

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
