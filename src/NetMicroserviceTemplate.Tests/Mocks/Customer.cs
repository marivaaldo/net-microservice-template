namespace NetMicroserviceTemplate.Tests.Mocks;

internal class Customer : Domain.Entities.Customer
{
    public static Domain.Entities.Customer New(string fullName = "", int age = -1, string email = "", Domain.ValueObjects.Address address = null)
        => new(fullName, age, email, address ?? Mocks.Address.New());
}
