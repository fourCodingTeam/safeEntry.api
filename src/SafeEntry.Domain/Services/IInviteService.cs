namespace SafeEntry.Domain.Services;

public interface IInviteService
{
    Task<int> GenerateCodeAsync(int residentId, int visitorId, DateTime startDate, int daysToExpiration, string justification);
    Task<bool> ValidateCodeAsync(int residentId, int visitorId, int code);
}
