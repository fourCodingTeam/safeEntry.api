using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories;

public class CondominiumRepository : ICondominiumRepository
{
    private readonly AppDbContext _context;

    public CondominiumRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Condominium?> GetCondominiumById(int condominiumId)
    {
        return await _context.Condominiums.FirstOrDefaultAsync(a => a.Id == condominiumId);
    }
}