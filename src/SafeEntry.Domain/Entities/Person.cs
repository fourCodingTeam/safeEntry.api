using SafeEntry.Domain.ValueObjects;

namespace SafeEntry.Domain.Entities;

public abstract class Person
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = null!;
    public Cpf CPF { get; protected set; } = null!;
    public long PhoneNumber { get; protected set; }

    protected Person() { }

    protected Person(string name, Cpf cpf, long phoneNumber)
    {
        Name = name;
        CPF = cpf;
        PhoneNumber = phoneNumber;
    }
}
