using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Enum;

namespace SafeEntry.Domain.Repositories;

public interface IResidentRespository
{
    Task<Resident> GetByIdAsync(int id);
    Task<IEnumerable<Resident>> GetAllAsync();
    Task AddAsync(Resident resident);
    Task UpdateAsync(Resident resident);
    Task DeleteAsync(int id);
    Task<IEnumerable<Resident>> GetByAddressIdAsync(int addressId);
}

