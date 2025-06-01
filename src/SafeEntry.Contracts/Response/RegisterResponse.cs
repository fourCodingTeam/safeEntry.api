namespace SafeEntry.Contracts.Response;

public record RegisterResponse(
    Guid UserId,
    string Email,
    int PersonId
);