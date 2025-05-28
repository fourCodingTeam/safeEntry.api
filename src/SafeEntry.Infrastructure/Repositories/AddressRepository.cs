using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Address>> GetByCondominiumId(int condominiumId)
    {
        return await _context.Addresses
            .Where(x => x.Id == condominiumId)
            .ToListAsync();
    }
}