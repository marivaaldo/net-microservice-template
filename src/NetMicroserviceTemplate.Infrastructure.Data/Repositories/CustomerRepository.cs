namespace NetMicroserviceTemplate.Infrastructure.Data.Repositories;

internal class CustomerRepository(ApplicationContext context) : EntityRepository<Customer>(context), ICustomerRepository
{
    public async Task<Customer> FindByEmailAsync(string email) => await _context.Customers.SingleOrDefaultAsync(x => x.Email == email);
}
