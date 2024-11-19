namespace NetMicroserviceTemplate.Application.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<PagedList<Customer>>
{
    public string[] Names { get; set; } = [];
    public string[] Emails { get; set; } = [];
    public int MinAge { get; set; } = 18;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
