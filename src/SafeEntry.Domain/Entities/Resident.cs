namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public int AddressId { get; private set; } 
    public Address Address { get; private set; }  
    protected Resident() { }

    public Resident(string name, long phoneNumber, Address address)
        : base(name, phoneNumber)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
        AddressId = address.Id;
    }
    public void UpdateAddress(Address address)
    {
        Address   = address;
        AddressId = address.Id;
    }
}
