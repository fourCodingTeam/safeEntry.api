using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IInviteRepository
{
    Task<bool> ExistsCodeForResidentAsync(int residentId, int code);
    Task AddAsync(Invite invite);
    Task<bool> ValidateCodeAsync(int residentId, int visitorId, int code);
    Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId);
    Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(int residentId, int visitorId, int code);
}