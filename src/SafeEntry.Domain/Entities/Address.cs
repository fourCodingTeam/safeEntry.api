namespace SafeEntry.Domain.Entities;

public class Address
{
    public int Id { get; protected set; }
    public Condominium Condominium { get; protected set; } = null!;
    public int CondominiumId { get; protected set; }
    public string? HomeStreet { get; protected set; }
    public int HomeNumber { get; protected set; }
    public ICollection<Resident> Residents { get; protected set; } = new List<Resident>();
    public int? HouseOwnerId { get; protected set; }
    public Resident? HouseOwner { get; protected set; }
    protected Address() { }

    public Address(Condominium condominium, string? homeStreet, int homeNumber)
    {
        Condominium = condominium ?? throw new ArgumentNullException(nameof(condominium));
        CondominiumId = condominium.Id;
        HomeStreet = string.IsNullOrWhiteSpace(homeStreet) ? null : homeStreet; ;
        HomeNumber = homeNumber;
    }

    public void SetHouseOwner(Resident newHouseOwner)
    {
        if (!Residents.Contains(newHouseOwner))
            throw new InvalidOperationException("Resident not Found on this address");

        foreach (var resident in Residents)
        {
            resident.SetAsHouseOwner(resident == newHouseOwner);
        }

        HouseOwner = newHouseOwner;
        HouseOwnerId = newHouseOwner.Id;
    }
}