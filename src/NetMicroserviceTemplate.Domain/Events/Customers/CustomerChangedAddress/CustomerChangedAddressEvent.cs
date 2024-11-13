using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Events.Customers.CustomerChangedAddress;

public class CustomerChangedAddressEvent(Customer customer) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public Customer Customer { get; } = customer ?? throw new DomainException("Customer cannot be null");
}
