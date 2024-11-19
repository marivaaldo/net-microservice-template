namespace NetMicroserviceTemplate.Application.Customers.Queries.GetCustomers;

internal class GetCustomersQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomersQuery, PagedList<Customer>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<PagedList<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetPagedAsync(request.PageNumber, request.PageSize, criteria => criteria
            .AddLikeArray<Customer>(x => x.FullName, request.Names)
            .AddLikeArray<Customer>(x => x.Email, request.Emails)
            .Add(Restrictions<Customer>.GreaterThanEqual(x => x.Age, request.MinAge))
            , cancellationToken);
    }
}
