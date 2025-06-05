namespace SafeEntry.Contracts.Responses;

public record AddressResponse(
    int Id,
    string? HomeStreet,
    int HomeNumber,
    string CondominiumName,
    int? HouseOwnerId,
    List<ResidentResponse> Residents
);