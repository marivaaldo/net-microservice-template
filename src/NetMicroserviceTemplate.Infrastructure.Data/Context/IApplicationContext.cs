namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

public interface IApplicationContext : IBaseContext, IUnitOfWork
{
    DbSet<Customer> Customers { get; set; }
}
