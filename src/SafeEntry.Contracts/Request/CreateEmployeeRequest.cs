namespace SafeEntry.Contracts.Request;

public record CreateEmployeeRequest
(
    string Name,
    long PhoneNumber,
    string Position,
    int CondominiumId,
    string Email,
    string Password
);