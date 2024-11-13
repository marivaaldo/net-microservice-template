using NetMicroserviceTemplate.Domain.Contracts.Repositories;

namespace NetMicroserviceTemplate.Domain.Events.Customers.CustomerRegistered;

internal class CustomerRegisteredEventHandler(ICustomerRepository customerRepository) : IDomainEventHandler<CustomerRegisteredEvent>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task HandleAsync(CustomerRegisteredEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // TODO Do something...
        var registeredCustomer = await _customerRepository.FindByIdAsync(domainEvent.Customer.Id, cancellationToken);
        //registeredCustomer.ChangeAddress(new Address(
        //    registeredCustomer.Address.Country + "_changed",
        //    registeredCustomer.Address.State,
        //    registeredCustomer.Address.City,
        //    registeredCustomer.Address.Neighborhood,
        //    registeredCustomer.Address.Street,
        //    registeredCustomer.Address.Number,
        //    registeredCustomer.Address.Complement,
        //    registeredCustomer.Address.PostalCode));
        //await _customerRepository.UpdateAsync(registeredCustomer);
    }
}
