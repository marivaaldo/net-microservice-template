namespace NetMicroserviceTemplate.Domain.Contracts;

public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task<bool> CompleteAsync(CancellationToken cancellationToken = default);
}
