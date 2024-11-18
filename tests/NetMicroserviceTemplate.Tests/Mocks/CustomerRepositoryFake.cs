using NetMicroserviceTemplate.Domain.Contracts.Repositories;
using NetMicroserviceTemplate.Infrastructure.Data.Context;
using NetMicroserviceTemplate.Infrastructure.Data.Repositories;

namespace NetMicroserviceTemplate.Tests.Mocks;

public class CustomerRepositoryFake(ApplicationContext context) : UnitOfWorkRepository<Domain.Entities.Customer>(context), ICustomerRepository
{
    public virtual IEnumerable<Customer> FindByEmail(string email)
    {
        throw new NotImplementedException();
    }
}
