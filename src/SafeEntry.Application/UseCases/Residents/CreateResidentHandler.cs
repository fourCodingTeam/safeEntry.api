using SafeEntry.Application.Interfaces;
using SafeEntry.Application.UseCases.Register;
using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Enums;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class CreateResidentHandler
    {
        private readonly IResidentRespository _repo;
        private readonly IAddressService _addressService;
        private readonly RegisterHandler _registerHandler;

        public CreateResidentHandler(IResidentRespository repo, IAddressService addressService, RegisterHandler registerHandler)
        {
            _repo = repo;
            _addressService = addressService;
            _registerHandler = registerHandler;
        }

        public async Task<ResidentResponse> Handle(CreateResidentRequest req)
        {
            var address = await _addressService.GetOrCreateAsync(req.CondominiumId, req.HomeNumber, req.HomeStreet);

            if (address == null)
                throw new ArgumentNullException(nameof(address));

            var resident = new Resident(req.Name, req.PhoneNumber, address, false);

            address.Residents.Add(resident);

            if (req.IsHomeOwner)
                address.SetHouseOwner(resident);

            await _repo.AddAsync(resident);
            await _addressService.UpdateAsync(address);

            var user = new RegisterRequest(resident.Id, req.Email, req.Password, UserTypeEnum.Resident);
            await _registerHandler.Handle(user);

            return new ResidentResponse(
                resident.Id,
                resident.Name,
                resident.PhoneNumber,
                resident.IsHomeOwner
            );
        }
    }
}
