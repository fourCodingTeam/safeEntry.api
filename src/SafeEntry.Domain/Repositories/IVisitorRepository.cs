namespace SafeEntry.Domain.Repositories;

using SafeEntry.Domain.Entities;

public interface IVisitorRespository
{
    Task<Visitor> GetByIdAsync(int id);
    Task<IEnumerable<Visitor>> GetAllAsync();
    Task AddAsync(Visitor visitor);
    Task UpdateAsync(Visitor visitor);
    Task DeleteAsync(int id);
}

