
namespace NetMicroserviceTemplate.Application.Customers.Queries.GetCustomers;

internal class GetCustomersQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    
    public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.FindAllAsync(cancellationToken);
    }
}
