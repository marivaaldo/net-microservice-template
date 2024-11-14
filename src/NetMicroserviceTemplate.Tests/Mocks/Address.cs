namespace NetMicroserviceTemplate.Tests.Mocks;

internal class Address : Domain.ValueObjects.Address
{
    public static Domain.ValueObjects.Address New(string country = "", string state = "", string city = "", string neighborhood = "", string street = "", string number = "", string complement = "", string postalCode = "")
        => new(country, state, city, neighborhood, street, number, complement, postalCode);
}
