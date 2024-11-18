namespace NetMicroserviceTemplate.Tests.UnitTests;

public class CustomerTests
{
    [Theory]
    [InlineData("name", 30, "email", "brasil")]
    [InlineData("name", 18, "email", "brasil")]
    public void Constructor(string fullName, int age, string email, string addressCountry)
    {
        var customer = Mocks.CustomerFake.New(fullName, age, email, Mocks.AddressFake.New(addressCountry));
        Assert.NotNull(customer);
    }

    [Theory]
    [InlineData("name", 18, "", "brasil")]
    [InlineData("", 18, "email", "brasil")]
    [InlineData("", 18, "", "brasil")]
    [InlineData("", 17, "", "brasil")]
    [InlineData("name", 17, "email", "")]
    [InlineData("name", 17, "email", "brasil")]
    public void ConstructorExceptions(string fullName, int age, string email, string addressCountry)
    {
        Assert.Throws<DomainException>(() => Mocks.CustomerFake.New(fullName, age, email, string.IsNullOrWhiteSpace(addressCountry) ? null : Mocks.AddressFake.New(addressCountry)));
    }

    [Fact]
    public void ChangeAddress()
    {
        var customer = Mocks.CustomerFake.New("name", 18, "mail@domain.com", Mocks.AddressFake.New("brasil"));

        Assert.Throws<DomainException>(() => customer.ChangeAddress(null));

        var currentAddress = customer.Address;
        customer.ChangeAddress(customer.Address);
        Assert.Equal(currentAddress, customer.Address);

        customer.ChangeAddress(Mocks.AddressFake.New("EUA"));
        Assert.NotEqual(currentAddress, customer.Address);
    }
}
