namespace NetMicroserviceTemplate.Application.Events.Customers.CustomerRegistered;

public class CustomerRegisteredIntegrationEvent(Guid customerId) : IntegrationEvent
{
    public Guid CustomerId { get; } = customerId;
}
