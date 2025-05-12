namespace SafeEntry.Domain.Repositories;

using SafeEntry.Domain.Entities;

public interface IInviteRepository
{
    Task<bool> ExistsCodeForResidentAsync(int residentId, int code);
    Task AddAsync(Invite invite);
    Task<bool> ValidateCodeAsync(int residentId, int visitorId, int code);
}