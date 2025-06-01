namespace SafeEntry.Contracts.Response;

public record UserResponse(
    Guid UserId,
    string Email,
    int PersonId
);
