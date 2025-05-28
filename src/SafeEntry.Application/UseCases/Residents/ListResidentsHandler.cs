using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeEntry.Application.Interfaces;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.ListResidents
{
    public class ListResidentsHandler
    {
        private readonly IResidentRespository _repo;
        public ListResidentsHandler(IResidentRespository repo)
            => _repo = repo;

        public async Task<IEnumerable<ResidentResponse>> Handle()
        {
            var residents = await _repo.GetAllAsync();

            return residents
                .Select(r => new ResidentResponse(
                    r.Id,
                    r.Name,
                    r.PhoneNumber
                ));
        }
    }
}
