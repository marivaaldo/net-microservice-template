using Microsoft.EntityFrameworkCore.Storage;
using NetMicroserviceTemplate.Domain.Events;
using System.Reflection;

namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

public abstract class BaseContext : DbContext, IBaseContext, IUnitOfWork
{
    private IDbContextTransaction _transaction;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public BaseContext(DbContextOptions<ApplicationContext> options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _domainEventDispatcher = domainEventDispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        RaiseDomainEventsAsync().Wait();
        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await RaiseDomainEventsAsync(cancellationToken);
        return result;
    }

    private async Task RaiseDomainEventsAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<IDomainEntity>()
            .SelectMany(x => x.Entity.ConsumeDomainEvents())
            .OrderBy(x => x.OccurredOn)
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
