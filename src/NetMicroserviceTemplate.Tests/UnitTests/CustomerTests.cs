﻿namespace NetMicroserviceTemplate.Tests.UnitTests;

public class CustomerTests
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
            Assert.Throws<DomainException>(() => Mocks.Customer.New(fullName, age, email, string.IsNullOrWhiteSpace(addressCountry) ? null : Mocks.Address.New(addressCountry)));
        }
        else
        {
            var customer = Mocks.Customer.New(fullName, age, email, Mocks.Address.New(addressCountry));
            Assert.NotNull(customer);
        }
    }

    [Fact]
    public void ChangeAddress()
    {
        var customer = Mocks.Customer.New("name", 18, "mail@domain.com", Mocks.Address.New("brasil"));

        Assert.Throws<DomainException>(() => customer.ChangeAddress(null));

        var currentAddress = customer.Address;
        customer.ChangeAddress(customer.Address);
        Assert.Equal(currentAddress, customer.Address);

        customer.ChangeAddress(Mocks.Address.New("EUA"));
        Assert.NotEqual(currentAddress, customer.Address);
    }
}