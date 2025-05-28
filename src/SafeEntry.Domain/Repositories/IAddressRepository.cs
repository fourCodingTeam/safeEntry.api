using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetByCondominiumId(int condominiumId);
}