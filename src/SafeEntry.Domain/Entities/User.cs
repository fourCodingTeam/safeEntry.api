using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Enums;
using SafeEntry.Domain.Services;
using SafeEntry.Domain.ValueObjects;

public class User
{
    public Guid Id { get; protected set; }
    public string Email { get; protected set; }
    public PasswordHash Password { get; protected set; }
    public int PersonId { get; protected set; }
    public Person Person { get; protected set; }
    public UserTypeEnum UserType { get; protected set; }
    public bool IsFirstLogin { get; protected set; }

    private User() { }

    public User(string email, PasswordHash password, Person person, UserTypeEnum userType)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        Person = person ?? throw new ArgumentNullException(nameof(person));
        PersonId = person.Id;
        UserType = userType;
        IsFirstLogin = true;
    }

    public void UpdatePassword(string newPassword, IPasswordHasher hasher)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("The password cannot be null");

        Password = new PasswordHash(hasher.Hash(newPassword));
        IsFirstLogin = false;
    }

    public bool ValidatePassword(string plainText, IPasswordHasher hasher)
        => hasher.Verify(plainText, Password.Value);
}
