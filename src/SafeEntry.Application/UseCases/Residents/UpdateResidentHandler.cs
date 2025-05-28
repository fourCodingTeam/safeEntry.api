using System.Threading.Tasks;
using SafeEntry.Contracts.Requests;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class UpdateResidentHandler
    {
        private readonly IResidentRepository _repo;
        public UpdateResidentHandler(IResidentRepository repo) => _repo = repo;

        public async Task<ResidentResponse?> Handle(UpdateResidentRequest req)
        {
            var existing = await _repo.GetByIdAsync(req.Id);
            if (existing is null) return null;

            var updatedAddress = new Address(
                req.Street,
                req.Number,
                req.Neighborhood,
                req.ZipCode,
                req.City,
                req.State,
                req.Country
            );

            existing.UpdateAddress(updatedAddress);
            existing.UpdateContact(req.Name, req.PhoneNumber);

            await _repo.UpdateAsync(existing);

            return new ResidentResponse(
                existing.Id,
                existing.Name,
                existing.PhoneNumber,
                new AddressResponse(
                    updatedAddress.Id,
                    updatedAddress.Street,
                    updatedAddress.Number,
                    updatedAddress.Neighborhood,
                    updatedAddress.ZipCode,
                    updatedAddress.City,
                    updatedAddress.State,
                    updatedAddress.Country
                )
            );
        }
    }
}
