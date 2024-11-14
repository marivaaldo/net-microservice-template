
namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

internal class CustomerRepository(IApplicationContext context) : EntityRepository<Customer>(context), ICustomerRepository
{
    public virtual async Task<Customer> FindByEmailAsync(string email) => await _context.Customers.SingleOrDefaultAsync(x => x.Email == email);
}
