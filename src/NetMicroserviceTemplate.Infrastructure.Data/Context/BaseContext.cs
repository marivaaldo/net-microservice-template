using Microsoft.EntityFrameworkCore.Storage;
using NetMicroserviceTemplate.Domain.Events;
using System.Reflection;

namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

internal abstract class BaseContext : DbContext, IUnitOfWork
{
    private IDbContextTransaction _transaction;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public BaseContext(DbContextOptions<ApplicationContext> options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        RaiseDomainEventsAsync().Wait();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await RaiseDomainEventsAsync(cancellationToken);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private async Task RaiseDomainEventsAsync(CancellationToken cancellationToken = default)
    {
        var changedStates = new[] { EntityState.Modified, EntityState.Added, EntityState.Deleted };
        var domainEvents = ChangeTracker.Entries<IDomainEntity>()
            .Where(x => changedStates.Contains(x.State))
            .SelectMany(x => x.Entity.ConsumeDomainEvents())
            .ToList();

        await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction = await Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) await Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) await Database.RollbackTransactionAsync(cancellationToken);
    }

    public async Task<bool> CompleteAsync(CancellationToken cancellationToken = default)
        => await SaveChangesAsync(cancellationToken) > 0;
}
