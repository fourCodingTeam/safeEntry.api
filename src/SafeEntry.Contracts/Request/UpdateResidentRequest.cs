namespace SafeEntry.Contracts.Request;

public record UpdateResidentRequest(
    int Id,
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
