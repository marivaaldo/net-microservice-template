using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Contracts.Repositories;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    IEnumerable<Customer> FindByEmail(string email);
}
