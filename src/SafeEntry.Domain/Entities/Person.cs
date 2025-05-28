namespace SafeEntry.Domain.Entities;

public class Person
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = null!;
    public long PhoneNumber { get; protected set; }
    public User? User { get; protected set; }

    protected Person() { }

    public Person(string name, long phoneNumber)
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
