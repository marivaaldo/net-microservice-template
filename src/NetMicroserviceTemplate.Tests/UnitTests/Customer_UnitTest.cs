namespace NetMicroserviceTemplate.Tests.UnitTests;

public class Customer_UnitTest
{
    [Theory]
    [InlineData("name", 30, "email")]
    [InlineData("name", 18, "email")]
    [InlineData("name", 18, "")]
    [InlineData("", 18, "email")]
    [InlineData("", 18, "")]
    [InlineData("", 17, "")]
    [InlineData("name", 17, "email")]
    public void NewCustomer(string fullName, int age, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName) || age < 18 || string.IsNullOrWhiteSpace(email))
        {
            Assert.Throws<DomainException>(() => new Customer(fullName, age, email, null));
        }
        else
        {
            var customer = new Customer(fullName, age, email, null);
            Assert.NotNull(customer);
        }
    }
}
