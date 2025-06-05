using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetByCondominiumId(int condominiumId);
    Task<Address?> GetByCondominiumIdAndNumber(int condominiumId, int homeNumber);
    Task<Address?> GetByResidentIdAsync(int residentId);
    Task<Address> AddAsync(Address address);
    Task UpdateAsync(Address address);
}