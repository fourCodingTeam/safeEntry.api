using System.Threading.Tasks;
using SafeEntry.Contracts.Requests;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Addresses
{
    public class UpdateAddressHandler
    {
        private readonly IAddressRepository _repo;
        public UpdateAddressHandler(IAddressRepository repo) => _repo = repo;

        public async Task<AddressResponse?> Handle(UpdateAddressRequest req)
        {
            var existing = await _repo.GetByIdAsync(req.Id);
            if (existing is null) return null;

            existing.Update(
                req.Street,
                req.Number,
                req.Neighborhood,
                req.ZipCode,
                req.City,
                req.State,
                req.Country
            );

            await _repo.UpdateAsync(existing);

            return new AddressResponse(
                existing.Id,
                existing.Street,
                existing.Number,
                existing.Neighborhood,
                existing.ZipCode,
                existing.City,
                existing.State,
                existing.Country
            );
        }
    }
}
