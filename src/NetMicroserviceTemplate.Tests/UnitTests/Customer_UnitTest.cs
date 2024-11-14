using NetMicroserviceTemplate.Domain.ValueObjects;

namespace NetMicroserviceTemplate.Tests.UnitTests;

public class Customer_UnitTest
{
    [Theory]
    [InlineData("name", 30, "email", "brasil")]
    [InlineData("name", 18, "email", "brasil")]
    [InlineData("name", 18, "", "brasil")]
    [InlineData("", 18, "email", "brasil")]
    [InlineData("", 18, "", "brasil")]
    [InlineData("", 17, "", "brasil")]
    [InlineData("name", 17, "email", "brasil")]
    [InlineData("name", 17, "email", "")]
    public void Constructor(string fullName, int age, string email, string addressCountry)
    {
        if (string.IsNullOrWhiteSpace(fullName) || age < 18 || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(addressCountry))
        {
            Assert.Throws<DomainException>(() => NewCustomer(fullName, age, email, string.IsNullOrWhiteSpace(addressCountry) ? null : NewAddress(addressCountry)));
        }
        else
        {
            var customer = new Customer(fullName, age, email, NewAddress(addressCountry));
            Assert.NotNull(customer);
        }
    }

    [Fact]
    public void ChangeAddress()
    {
        var customer = NewCustomer("name", 18, "mail@domain.com", NewAddress("brasil"));

        Assert.Throws<DomainException>(() => customer.ChangeAddress(null));

        var currentAddress = customer.Address;
        customer.ChangeAddress(customer.Address);
        Assert.Equal(currentAddress, customer.Address);

        customer.ChangeAddress(NewAddress("eua"));
        Assert.NotEqual(currentAddress, customer.Address);
    }

    private static Customer NewCustomer(string fullName = "", int age = -1, string email = "", Address address = null)
        => new Customer(fullName, age, email, address ?? NewAddress());

    private static Address NewAddress(string country = "", string state = "", string city = "", string neighborhood = "", string street = "", string number = "", string complement = "", string postalCode = "")
        => new Address(country, state, city, neighborhood, street, number, complement, postalCode);
}
