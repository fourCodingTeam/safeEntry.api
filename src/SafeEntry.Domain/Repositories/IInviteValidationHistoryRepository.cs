using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IInviteValidationHistoryRepository
{
    Task AddAsync(InviteValidationHistory inviteHistory);
    Task<IEnumerable<InviteValidationHistory>> GetAllAsync(int condominiumId);
    Task<IEnumerable<InviteValidationHistory>> GetLastSevenDaysAsync(int condominiumId);
}