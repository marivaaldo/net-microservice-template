namespace NetMicroserviceTemplate.Application.Customers.Queries.GetCustomers;

internal class GetCustomersQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return _customerRepository.FindAsync(criteria => criteria
            .AddLikeArray<Customer>(x => x.FullName, request.Names)
            .AddLikeArray<Customer>(x => x.Email, request.Emails)
            .Add(Restrictions<Customer>.GreaterThanEqual(x => x.Age, request.MinAge))
            , cancellationToken);
    }
}
