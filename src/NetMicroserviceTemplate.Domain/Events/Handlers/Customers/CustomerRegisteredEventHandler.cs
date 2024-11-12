using NetMicroserviceTemplate.Domain.Contracts.Repositories;
using NetMicroserviceTemplate.Domain.Events.Customers;

namespace NetMicroserviceTemplate.Domain.Events.Handlers.Customers;

internal class CustomerRegisteredEventHandler(ICustomerRepository customerRepository) : IDomainEventHandler<CustomerRegisteredEvent>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task HandleAsync(CustomerRegisteredEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var registeredCustomer = await _customerRepository.FindByIdAsync(domainEvent.Customer.Id, cancellationToken);
        // TODO Do something...
    }
}
