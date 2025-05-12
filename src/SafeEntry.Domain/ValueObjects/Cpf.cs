using System.Text.RegularExpressions;

namespace SafeEntry.Domain.ValueObjects;

public class Cpf
{
    public string Value { get; }

    public Cpf(string value)
    {
        if (!Regex.IsMatch(value, @"^\d{11}$"))
            throw new ArgumentException("Invalid CPF.");
        Value = value;
    }

    public override string ToString() => Value;
}
