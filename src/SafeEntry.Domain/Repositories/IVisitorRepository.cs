using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IVisitorRespository
{
    //Task<Visitor> GetByIdAsync(int id);
    Task<Visitor?> GetByNameAndPhoneAsync(string name, long phoneNumber);
    //Task<IEnumerable<Visitor>> GetAllAsync();
    Task<Visitor> AddAsync(Visitor visitor);
    Task<Visitor> GetOrCreateAsync(string name, long phoneNumber);
    //Task UpdateAsync(Visitor visitor);
    //Task DeleteAsync(int id);
}

