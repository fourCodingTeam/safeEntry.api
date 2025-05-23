using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Services;

public interface IInviteService
{
    Task<int> GenerateCodeAsync(GenerateInviteRequest request);
    Task<bool> ValidateCodeAsync(ValidateInviteRequest request);
    Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId);
    Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(ValidateInviteRequest request);
}
