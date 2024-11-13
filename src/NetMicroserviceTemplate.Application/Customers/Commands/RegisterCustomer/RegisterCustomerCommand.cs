namespace NetMicroserviceTemplate.Application.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommand : IRequest<Guid>
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}
