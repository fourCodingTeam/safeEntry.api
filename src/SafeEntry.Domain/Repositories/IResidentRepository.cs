namespace SafeEntry.Domain.Repositories;

using SafeEntry.Domain.Entities;

public interface IResidentRespository
{
    Task<Resident> GetByIdAsync(int id);
    Task<IEnumerable<Resident>> GetAllAsync();
    Task AddAsync(Resident resident);
    Task UpdateAsync(Resident resident);
    Task DeleteAsync(int id);
}

