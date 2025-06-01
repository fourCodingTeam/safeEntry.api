namespace SafeEntry.Contracts.Requests
{
    public record CreateResidentRequest(
        string Name,
        long PhoneNumber,
        int CondominiumId,
        string? HomeStreet,
        int HomeNumber
    );
}
