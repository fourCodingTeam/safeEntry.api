using SafeEntry.Domain.Enum;

namespace SafeEntry.Contracts.Responses;

public record UpdateStatusResidentResponse(
    int Id,
    StatusResident Status
);