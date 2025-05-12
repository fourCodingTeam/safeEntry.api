using SafeEntry.Domain.ValueObjects;

namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public Address Address { get; protected set; } = null!;

    protected Resident() { }

    public Resident(string name, Cpf cpf, long phoneNumber, Address address)
        : base(name, cpf, phoneNumber)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

}
