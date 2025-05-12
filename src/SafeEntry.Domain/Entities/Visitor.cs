using SafeEntry.Domain.ValueObjects;

namespace SafeEntry.Domain.Entities;

public class Visitor : Person
{
	public Visitor(string name, Cpf cpf, long phoneNumber) : base(name, cpf, phoneNumber) {  }

    protected Visitor(){ }
}
