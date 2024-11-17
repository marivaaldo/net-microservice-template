namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

public interface IBaseContext : IUnitOfWork
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
}
