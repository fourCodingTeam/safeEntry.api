namespace SafeEntry.Domain.Entities;

public class Address
{
    public int Id { get; protected set; }
    public string Street { get; protected set; }
    public int Number { get; protected set; }
    public string District { get; protected set; }
    public string ZipCode { get; protected set; }
    public string City { get; protected set; }
    public string State { get; protected set; }
    public string Country { get; protected set; }

    protected Address() { }

    public Address(string street, int number, string district, string zipCode, string city, string state, string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        Number = number;
        District = district ?? throw new ArgumentNullException(nameof(district));
        ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }
}
