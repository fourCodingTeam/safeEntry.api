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
        private readonly IResidentRepository _repo;
        public CreateResidentHandler(IResidentRepository repo) => _repo = repo;

        public async Task<ResidentResponse> Handle(CreateResidentRequest req)
        {
            var address = new Address(
                req.Street, req.Number, req.Neighborhood,
                req.ZipCode, req.City, req.State, req.Country);
            var resident = new Resident(req.Name, req.PhoneNumber, address);
            await _repo.AddAsync(resident);

            return new ResidentResponse(
                resident.Id,
                resident.Name,
                resident.PhoneNumber,
                new AddressResponse(
                    address.Id,
                    address.Street,
                    address.Number,
                    address.Neighborhood,
                    address.ZipCode,
                    address.City,
                    address.State,
                    address.Country
                )
            );
        }
    }
}
