namespace SafeEntry.Domain.Entities;

public abstract class Person
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = null!;
    public long PhoneNumber { get; protected set; }
    public User? User { get; protected set; }

    protected Person() { }

    protected Person(string name, long phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }

    public void SetUser(User user)
    {
        User = user;
    }
    public void UpdateContact(string name, long phoneNumber)
    {
       Name        = name;
       PhoneNumber = phoneNumber;
    }
}
