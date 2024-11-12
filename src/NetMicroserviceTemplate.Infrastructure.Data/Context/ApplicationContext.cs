using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

internal sealed class ApplicationContext(DbContextOptions<ApplicationContext> options, IDomainEventDispatcher domainEventDispatcher) : BaseContext(options, domainEventDispatcher)
{
    public DbSet<Customer> Customers { get; set; }
}
