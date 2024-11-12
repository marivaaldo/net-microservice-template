using NetMicroserviceTemplate.Domain.Entities;

namespace NetMicroserviceTemplate.Domain.Events;

public class CustomerChangedAddressEvent(Customer customer) : DomainEvent
{
    public Customer Customer { get; } = customer ?? throw new DomainException("Customer cannot be null");
}
