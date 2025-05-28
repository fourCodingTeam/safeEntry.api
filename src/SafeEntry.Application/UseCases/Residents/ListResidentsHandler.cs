using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeEntry.Application.Interfaces;
using SafeEntry.Contracts.Response  ;
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
            return residents.Select(r =>
                new ResidentResponse(
                    r.Id,
                    r.Name,
                    r.PhoneNumber,
                    new AddressResponse(
                            r.Address.Id,
                           r.Address.Street,
                           r.Address.Number,
                           r.Address.Neighborhood,
                           r.Address.ZipCode,
                           r.Address.City,
                           r.Address.State,
                           r.Address.Country
                    )
                )
            );
        }
    }
}
