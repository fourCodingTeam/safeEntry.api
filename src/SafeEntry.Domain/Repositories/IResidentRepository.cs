using System.Collections.Generic;
using System.Threading.Tasks;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories
{
    public interface IResidentRepository
    {
        Task<IEnumerable<Resident>> GetAllAsync();
        Task<Resident?> GetByIdAsync(int id);
        Task AddAsync(Resident resident);
        Task UpdateAsync(Resident resident);
        Task DeleteAsync(int id);
    }
}
