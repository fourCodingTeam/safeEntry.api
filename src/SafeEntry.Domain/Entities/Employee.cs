namespace SafeEntry.Domain.Entities;

public class Employee : Person 
{
    public string Position { get; protected set; } = null!;
    public Condominium Condominium { get; protected set; } = null!;
    public Employee(string name, long phoneNumber, string position, Condominium condominium) : base(name, phoneNumber)
    {
        Position = position ?? throw new ArgumentNullException(nameof(position));
        Condominium = condominium ?? throw new ArgumentNullException(nameof(condominium));
    }

    protected Employee() { }
}
