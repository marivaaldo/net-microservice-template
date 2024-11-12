using NetMicroserviceTemplate.Domain.Events.Customers;
using NetMicroserviceTemplate.Domain.ValueObjects;

namespace NetMicroserviceTemplate.Domain.Entities;

public class Customer : Entity
{
    public string FullName { get; protected set; }
    public int Age { get; protected set; }
    public string Email { get; protected set; }
    public Address Address { get; protected set; }

    protected Customer() { }

    public Customer(string fullName, int age, string email, Address address)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Age = age;
        Email = email;
        Address = address;

        if (string.IsNullOrWhiteSpace(FullName))
            throw new DomainException("FullName can't be empty");

        if (string.IsNullOrWhiteSpace(Email))
            throw new DomainException("Email can't be empty");

        if (Age < 18)
            throw new DomainException("Age can't less than 18 years");

        AddDomainEvent(new CustomerRegisteredEvent(this));
    }

    public void ChangeAddress(Address address)
    {
        if (address == null)
            throw new DomainException("Address cannot be null");

        if (Address == address)
            return;

        Address = address;

        AddDomainEvent(new CustomerChangedAddressEvent(this));
    }
}
