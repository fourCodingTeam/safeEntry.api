namespace SafeEntry.Contracts.Responses;

public record EmployeeResponse(
    int Id,
    string Name,
    string Position,
    string CondominiumName
);