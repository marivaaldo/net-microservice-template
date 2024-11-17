using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

public class ApplicationContext(DbContextOptions<ApplicationContext> options, IDomainEventDispatcher domainEventDispatcher) : BaseContext(options, domainEventDispatcher), IApplicationContext
{
    public DbSet<Customer> Customers { get; set; }
}
