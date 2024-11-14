namespace NetMicroserviceTemplate.Infrastructure.Data.Context;

internal interface IApplicationContext : IBaseContext, IUnitOfWork
{
    DbSet<Customer> Customers { get; set; }
}
