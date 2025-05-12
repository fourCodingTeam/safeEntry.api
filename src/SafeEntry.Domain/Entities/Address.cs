namespace SafeEntry.Domain.Entities;

public class Address
{
    public int Id { get; protected set; }
    public string Street { get; protected set; } = null!;
    public int Number { get; protected set; }
    public string Neighborhood { get; protected set; } = null!;
    public string ZipCode { get; protected set; } = null!;
    public string City { get; protected set; } = null!;
    public string State { get; protected set; } = null!;
    public string Country { get; protected set; } = null!;

    protected Address() { }

    public Address(string street, int number, string neighborhood, string zipCode, string city, string state, string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        Number = number;
        Neighborhood = neighborhood ?? throw new ArgumentNullException(nameof(neighborhood));
        ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }
}
