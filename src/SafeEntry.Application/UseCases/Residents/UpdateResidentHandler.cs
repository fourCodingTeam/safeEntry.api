using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class UpdateResidentHandler
    {
        private readonly IResidentRepository _repo;
        public UpdateResidentHandler(IResidentRepository repo) => _repo = repo;

        public async Task<ResidentResponse?> Handle(UpdateResidentRequest req)
        {
            var existing = await _repo.GetByIdAsync(req.Id);
            if (existing is null) return null;

            existing.UpdateContact(req.Name, req.PhoneNumber);

            await _repo.UpdateAsync(existing);

            return new ResidentResponse(
                existing.Id,
                existing.Name,
                existing.PhoneNumber,
                existing.IsHomeOwner,
                existing.StatusResident
            );
        }
    }
}
