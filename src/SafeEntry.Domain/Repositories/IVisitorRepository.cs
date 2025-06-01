using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IVisitorRespository
{
    Task<Visitor?> GetByNameAndPhoneAsync(string name, long phoneNumber);
    Task<Visitor> AddAsync(Visitor visitor);
    Task<Visitor> GetOrCreateAsync(string name, long phoneNumber);
}

