namespace SafeEntry.Contracts.Request;

public record UpdatePasswordRequest(
    string Email,
    string Password,
    string ConfirmPassword
);