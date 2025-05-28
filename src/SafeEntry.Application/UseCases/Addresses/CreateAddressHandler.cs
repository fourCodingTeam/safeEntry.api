using System.Threading.Tasks;
using SafeEntry.Contracts.Requests;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Addresses
{
    public class CreateAddressHandler
    {
        private readonly IAddressRepository _repo;
        public CreateAddressHandler(IAddressRepository repo) => _repo = repo;

        public async Task<AddressResponse> Handle(CreateAddressRequest req)
        {
            var address = new Address(
                req.Street, req.Number, req.Neighborhood,
                req.ZipCode, req.City, req.State, req.Country
            );
            await _repo.AddAsync(address);

            return new AddressResponse(
                address.Id,
                address.Street,
                address.Number,
                address.Neighborhood,
                address.ZipCode,
                address.City,
                address.State,
                address.Country
            );
        }
    }
}
