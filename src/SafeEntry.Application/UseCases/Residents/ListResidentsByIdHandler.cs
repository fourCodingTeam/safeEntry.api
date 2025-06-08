using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.UseCases.ListResidents
{
    public class ListResidentsByIdHandler
    {
        private readonly IResidentRepository _repo;
        public ListResidentsByIdHandler(IResidentRepository repo)
            => _repo = repo;

        public async Task<Resident> Handle(int residentId)
        {
            var resident = await _repo.GetByIdAsync(residentId);

            if (resident is null)
                throw new ArgumentNullException("Resident not Found");

            return resident;
        }
    }
}
