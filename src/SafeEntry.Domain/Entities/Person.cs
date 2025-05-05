namespace SafeEntry.Domain.Entities;

public abstract class Person
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = null;
    public long CPF { get; protected set; } = null;
    public long PhoneNumber { get; protected set; } = null;

    protected Person() { }

    protected Person(string name, long cpf, string phoneNumber)
    {
        Name = name;
        CPF = cpf;
        PhoneNumber = phoneNumber;
    }
}
