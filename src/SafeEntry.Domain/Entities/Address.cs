namespace SafeEntry.Domain.Entities;

public class Address
{
    public int Id { get; protected set; }
    public Condominium Condominium { get; protected set; } = null!;
    public string? HomeStreet { get; protected set; }
    public int HomeNumber { get; protected set; }
    protected Address() { }

    public Address(Condominium condominium, string? homeStreet, int homeNumber)
    {
        Condominium = condominium ?? throw new ArgumentNullException(nameof(condominium));
        HomeStreet = string.IsNullOrWhiteSpace(homeStreet) ? null : homeStreet; ;
        HomeNumber = homeNumber;
    }
}