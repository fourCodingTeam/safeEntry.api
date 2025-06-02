using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.UseCases.ListResidents
{
    public class ListResidentsByAddressIdHandler
    {
        private readonly IResidentRespository _repo;
        public ListResidentsByAddressIdHandler(IResidentRespository repo)
            => _repo = repo;

        public async Task<IEnumerable<Resident>> Handle(int addressId)
        {
            var residents = await _repo.GetByAddressIdAsync(addressId);

            return residents;
        }
    }
}
