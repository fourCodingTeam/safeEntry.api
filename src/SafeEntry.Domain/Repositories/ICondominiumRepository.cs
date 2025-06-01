using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface ICondominiumRepository
{
    Task<Condominium?> GetCondominiumById(int id);
}