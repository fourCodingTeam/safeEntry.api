using SafeEntry.Domain.ValueObjects;

namespace SafeEntry.Domain.Entities;

public class Employee : Person 
{
    public Employee(string name, Cpf cpf, long phoneNumber) : base(name, cpf, phoneNumber) {    }

    protected Employee() { }
}
