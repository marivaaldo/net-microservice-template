namespace NetMicroserviceTemplate.Domain.Events.Customers.CustomerRegistered;

public class CustomerRegisteredEvent(Guid customerId) : DomainEvent
{
    public Guid CustomerId { get; } = customerId;
}
