using System.Collections.Generic;
using System.Threading.Tasks;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IResidentRespository
{
    Task<Resident> GetByIdAsync(int id);
    Task<IEnumerable<Resident>> GetAllAsync();
    Task AddAsync(Resident resident);
    Task UpdateAsync(Resident resident);
    Task DeleteAsync(int id);
}

