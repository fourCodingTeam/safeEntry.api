using SafeEntry.Domain.Enum;

namespace SafeEntry.Contracts.Responses;

public record ResidentResponse(
    int Id,
    string Name,
    long PhoneNumber,
    bool IsHomeOwner,
    StatusResident status
);