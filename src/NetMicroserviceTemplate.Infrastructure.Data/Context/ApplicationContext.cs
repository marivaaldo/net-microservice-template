using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

internal class ApplicationContext(DbContextOptions<ApplicationContext> options, IDomainEventDispatcher domainEventDispatcher) : BaseContext(options, domainEventDispatcher), IApplicationContext
{
    public DbSet<Customer> Customers { get; set; }
}
