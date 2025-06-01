using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAddressesByEmployeeId(int employeeId);
    Task<Address> GetOrCreateAsync(int condominiumId, int homeNumber, string? homeStreet);
}