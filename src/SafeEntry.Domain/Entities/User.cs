using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Services;
using SafeEntry.Domain.ValueObjects;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public PasswordHash Password { get; private set; }

    public int PersonId { get; private set; }
    public Person Person { get; private set; }

    private User() { } // EF Core

    public User(string email, PasswordHash password, Person person)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        Person = person ?? throw new ArgumentNullException(nameof(person));
        PersonId = person.Id;
    }

    public bool ValidatePassword(string plainText, IPasswordHasher hasher)
        => hasher.Verify(plainText, Password.Value);
}
