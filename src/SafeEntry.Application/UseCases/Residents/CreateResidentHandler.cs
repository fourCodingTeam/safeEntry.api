using System;
using System.Threading.Tasks;
using SafeEntry.Contracts.Requests;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class CreateResidentHandler
    {
        private readonly IResidentRespository _repo;
        public CreateResidentHandler(IResidentRespository repo) => _repo = repo;

        public async Task<ResidentResponse> Handle(CreateResidentRequest req)
        {
            
            var resident = new Resident(req.Name, req.PhoneNumber);
            await _repo.AddAsync(resident);

            return new ResidentResponse(
                resident.Id,
                resident.Name,
                resident.PhoneNumber
            );
        }
    }
}
