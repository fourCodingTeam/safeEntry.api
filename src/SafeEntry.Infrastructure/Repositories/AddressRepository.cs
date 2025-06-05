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
            .Include(a => a.HouseOwner)
            .Include(a => a.Residents)
            .Include(a => a.Condominium)
            .Where(x => x.CondominiumId == condominiumId)
            .ToListAsync();
    }

    public async Task<Address?> GetByResidentIdAsync(int residentId)
    {
        return await _context.Addresses
            .Include(a => a.Residents)
            .FirstOrDefaultAsync(a => a.Residents.Any(r => r.Id == residentId));
    }

    public async Task<Address?> GetByCondominiumIdAndNumber(int condominiumId, int homeNumber)
    {
        return await _context.Addresses
            .Include(a => a.Residents)
            .FirstOrDefaultAsync(a => a.Condominium.Id == condominiumId && a.HomeNumber == homeNumber);
    }

    public async Task<Address> AddAsync(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }
}