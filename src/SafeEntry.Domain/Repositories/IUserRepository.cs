using System.Threading.Tasks;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
