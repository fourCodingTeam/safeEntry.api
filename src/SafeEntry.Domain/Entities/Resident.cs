namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public Address Address { get; protected set; } = null!;

    protected Resident() { }

    public Resident(string name, long phoneNumber, Address address)
        : base(name, phoneNumber)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

}
