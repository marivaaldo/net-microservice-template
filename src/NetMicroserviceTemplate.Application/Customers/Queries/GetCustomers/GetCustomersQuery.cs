namespace NetMicroserviceTemplate.Application.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IEnumerable<Customer>>
{
    public string[] Names { get; set; } = [];
    public string[] Emails { get; set; } = [];
    public int MinAge { get; set; } = 18;
}
