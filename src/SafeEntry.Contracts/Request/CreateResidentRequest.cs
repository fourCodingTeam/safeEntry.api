namespace SafeEntry.Contracts.Request
{
    public record CreateResidentRequest(
        string Name,
        long PhoneNumber,
        int CondominiumId,
        string? HomeStreet,
        int HomeNumber,
        string Email,
        string Password,
        bool IsHomeOwner
    );
}
