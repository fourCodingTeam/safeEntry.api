namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public int AddressId { get; private set; } 
    public Address Address { get; private set; }  
    protected Resident() { }

    public Resident(string name, long phoneNumber)
        : base(name, phoneNumber)
    {
        
    }
}
