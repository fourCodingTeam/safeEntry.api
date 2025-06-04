namespace SafeEntry.Contracts.Response;
public record LoginResponse(string Token, DateTime ExpiresAt, bool IsFirstLogin);