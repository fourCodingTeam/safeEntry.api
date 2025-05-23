namespace SafeEntry.Domain.Entities;

public class Employee : Person 
{
    public string Position { get; protected set; } = null!;
    public Employee(string name, long phoneNumber, string position) : base(name, phoneNumber)
    {
        Position = position ?? throw new ArgumentNullException(nameof(position)); ;
    }

    protected Employee() { }
}
