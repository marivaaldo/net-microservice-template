using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Contracts.Repositories;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    Task<Customer> FindByEmailAsync(string email);
}
