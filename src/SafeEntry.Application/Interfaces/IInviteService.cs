using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Services;

public interface IInviteService
{
    Task<int> GenerateCodeAsync(GenerateInviteRequest request);
    Task<bool> ValidateCodeAsync(ValidateInviteRequest request);
    Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId);
    Task<IEnumerable<Invite>> GetInvitesByAddressIdAsync(int addressId);
    Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(int residentId, int vistorId, int code);
    Task<Invite> GetInviteByAddressIdAndVisitorIdAsync(int addressId, int vistorId, int code);
    Task<long> CountInvitesByAddressIdAsync(int addressId);
}
