using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IInviteRepository
{
    Task<bool> ExistsCodeForAddressAsync(int addressId, int code);
    Task AddAsync(Invite invite);
    Task<bool> ValidateCodeAsync(int addressId, int visitorId, int code, DateTime dateNow);
    Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId);
    Task<IEnumerable<Invite>> GetInvitesByAddressIdAsync(int addressId);
    Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(int residentId, int visitorId, int code);
    Task<Invite> GetInviteByAddressIdAndVisitorIdAsync(int addressId, int visitorId, int code);
    Task<long> CountByAddressIdAsync(int addressId);
}