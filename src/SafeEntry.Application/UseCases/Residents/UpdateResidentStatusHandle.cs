using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class UpdadeResidentStatusHandler
    {
        private readonly IResidentRespository _repo;
        public UpdadeResidentStatusHandler(IResidentRespository repo) => _repo = repo;

        public async Task<UpdateStatusResidentResponse?> Handle(UpdateResidentStatusRequest req)
        {
            var existing = await _repo.GetByIdAsync(req.ResidentId);
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
