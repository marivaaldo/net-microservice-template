namespace NetMicroserviceTemplate.Domain.Events.Customers.CustomerChangedAddress;

public class CustomerChangedAddressEvent(Guid customerId) : DomainEvent
{
    public Guid CustomerId { get; } = customerId;
}
