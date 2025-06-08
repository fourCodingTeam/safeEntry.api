using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<AddressResponse>> GetAddressesByEmployeeId(int employeeId);
    Task<Address> GetOrCreateAsync(int condominiumId, int homeNumber, string? homeStreet);
    Task UpdateAsync(Address address);
    Task<Address> GetByResidentIdAsync(int residentId);
}