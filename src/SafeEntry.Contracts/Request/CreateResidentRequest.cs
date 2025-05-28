namespace SafeEntry.Contracts.Requests
{
    public record CreateResidentRequest(
        string Street,
        int Number,
        string Neighborhood,
        string ZipCode,
        string City,
        string State,
        string Country,
        string Name,
        long PhoneNumber
    );
}
