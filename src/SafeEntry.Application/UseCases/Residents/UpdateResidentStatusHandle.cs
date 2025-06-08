using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class UpdadeResidentStatusHandler
    {
        private readonly IResidentRepository _repo;
        public UpdadeResidentStatusHandler(IResidentRepository repo) => _repo = repo;

        public async Task<UpdateStatusResidentResponse?> Handle(int residentId,UpdateResidentStatusRequest req)
        {
            var existing = await _repo.GetByIdAsync(residentId);
            if (existing is null) return null;

            existing.UpdateResidentStatus(req.NewStatus);

            await _repo.UpdateAsync(existing);

            return new UpdateStatusResidentResponse(
                existing.Id,
                existing.StatusResident
            );
        }
    }
}
