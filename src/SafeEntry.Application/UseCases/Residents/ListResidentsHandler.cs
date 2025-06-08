using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.ListResidents
{
    public class ListResidentsHandler
    {
        private readonly IResidentRepository _repo;
        public ListResidentsHandler(IResidentRepository repo)
            => _repo = repo;

        public async Task<IEnumerable<ResidentResponse>> Handle()
        {
            var residents = await _repo.GetAllAsync();

            return residents
                .Select(r => new ResidentResponse(
                    r.Id,
                    r.Name,
                    r.PhoneNumber,
                    r.IsHomeOwner,
                    r.StatusResident
                ));
        }
    }
}
