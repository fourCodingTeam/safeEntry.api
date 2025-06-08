using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.Interfaces
{
    public interface IInviteValidationHistoryService
    {
        Task AddAsync(InviteHistoryRequest request);
        Task<IEnumerable<InviteValidationHistory>> GetAllAsync(int condominiumId);
        Task<IEnumerable<InviteValidationHistory>> GetLastSevenDaysAsync(int condominiumId);
    }
}
