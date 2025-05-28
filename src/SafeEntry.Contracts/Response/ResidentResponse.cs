namespace SafeEntry.Contracts.Responses
{
    public record ResidentResponse(
        int Id,
        string Name,
        long PhoneNumber,
        AddressResponse Address
    );
}