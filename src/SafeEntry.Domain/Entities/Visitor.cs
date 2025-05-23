namespace SafeEntry.Domain.Entities;

public class Visitor : Person
{
	public Visitor(string name, long phoneNumber) : base(name, phoneNumber) {  }

    protected Visitor(){ }
}
