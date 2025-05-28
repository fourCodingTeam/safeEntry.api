using System.Threading.Tasks;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
    }
}
