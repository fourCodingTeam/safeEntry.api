using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories;

public class ResidentRepository : IResidentRespository
{
    private readonly AppDbContext _context;

    public ResidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Resident> AddAsync(Resident resident)
    {
        _context.Residents.Add(resident);
        await _context.SaveChangesAsync();
        return resident;
    }
}
