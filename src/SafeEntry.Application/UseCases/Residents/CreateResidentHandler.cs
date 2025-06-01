using SafeEntry.Application.Interfaces;
using SafeEntry.Contracts.Requests;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class CreateResidentHandler
    {
        private readonly IResidentRespository _repo;
        private readonly IAddressService _addressService;
        public CreateResidentHandler(IResidentRespository repo, IAddressService addressService)
        {
            _repo = repo;
            _addressService = addressService;
        }

        public async Task<ResidentResponse> Handle(CreateResidentRequest req)
        {
            var address = await _addressService.GetOrCreateAsync(req.CondominiumId, req.HomeNumber, req.HomeStreet);

            if (address == null)
                throw new ArgumentNullException(nameof(address));

            var resident = new Resident(req.Name, req.PhoneNumber, address);
            await _repo.AddAsync(resident);

            return new ResidentResponse(
                resident.Id,
                resident.Name,
                resident.PhoneNumber
            );
        }
    }
}
