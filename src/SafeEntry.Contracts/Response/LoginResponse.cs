using SafeEntry.Domain.Enums;

namespace SafeEntry.Contracts.Response;
public record LoginResponse(string Token, DateTime ExpiresAt, bool IsFirstLogin, int PersonId, string PersonName, UserTypeEnum Role);