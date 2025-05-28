namespace SafeEntry.Contracts.Request
{
    public record RegisterRequest(
        string Name,
        long PhoneNumber,
        string Email,
        string Password
    );
}