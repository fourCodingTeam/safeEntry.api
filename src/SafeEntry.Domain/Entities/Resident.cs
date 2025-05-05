namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public Address address { get; protected set; }

    public Resident(string name, long cpf, long phoneNumber, Address address)
        : base(name, cpf, phoneNumber)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

}
