namespace SafeEntry.Domain.Entities;

public class Condominium
{
    public int Id { get; protected set; }
    public string Street { get; protected set; } = null!;
    public int Number { get; protected set; }
    public string Neighborhood { get; protected set; } = null!;
    public string ZipCode { get; protected set; } = null!;
    public string City { get; protected set; } = null!;
    public string State { get; protected set; } = null!;
    public string Country { get; protected set; } = null!;

    public Condominium() { }

    public Condominium(int id, string street, int number, string neighborhood, string zipCode, string city, string state, string country)
    {
        Id = id;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        ZipCode = zipCode;
        City = city;
        State = state;
        Country = country;
    }
}