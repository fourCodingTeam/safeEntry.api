using SafeEntry.Domain.Enums;

namespace SafeEntry.Contracts.Request;

public record RegisterRequest(
    int PersonId,
    string Email,
    string Password,
    UserTypeEnum UserType
);