namespace SafeEntry.Domain.ValueObjects;

public sealed class PasswordHash
{
    public string Value { get; }

    private PasswordHash() { }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Hash inválido", nameof(value));
        Value = value;
    }

    public override bool Equals(object obj)
        => obj is PasswordHash other && Value == other.Value;

    public override int GetHashCode()
        => Value.GetHashCode();
}
