
namespace NetMicroserviceTemplate.Domain.ValueObjects;

public class Address
{
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string PostalCode { get; set; }

    protected Address() { }

    public Address(string country, string state, string city, string neighborhood, string street, string number, string complement, string postalCode)
    {
        Country = country;
        State = state;
        City = city;
        Neighborhood = neighborhood;
        Street = street;
        Number = number;
        Complement = complement;
        PostalCode = postalCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is Address address &&
               Country == address.Country &&
               State == address.State &&
               City == address.City &&
               Neighborhood == address.Neighborhood &&
               Street == address.Street &&
               Number == address.Number &&
               Complement == address.Complement &&
               PostalCode == address.PostalCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Country, State, City, Neighborhood, Street, Number, Complement, PostalCode);
    }

    public static bool operator ==(Address? left, Address? right)
    {
        return EqualityComparer<Address>.Default.Equals(left, right);
    }

    public static bool operator !=(Address? left, Address? right)
    {
        return !(left == right);
    }
}
