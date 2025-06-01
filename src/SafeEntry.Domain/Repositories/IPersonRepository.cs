using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(int personId);
}
