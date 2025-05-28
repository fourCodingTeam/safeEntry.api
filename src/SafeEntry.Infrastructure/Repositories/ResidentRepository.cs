using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ResidentRepository : IResidentRepository
{
    private readonly AppDbContext _ctx;
    public ResidentRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Resident>> GetAllAsync()
        => await _ctx.Persons
            .OfType<Resident>()
            .Include(r => r.Address)
            .AsNoTracking()
            .ToListAsync();

    public Task<Resident?> GetByIdAsync(int id)
        => _ctx.Persons
               .OfType<Resident>()
               .Include(r => r.Address)
               .SingleOrDefaultAsync(r => r.Id == id);

    public async Task AddAsync(Resident resident)
    {
        _ctx.Persons.Add(resident);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Resident resident)
    {
        _ctx.Persons.Update(resident);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return;
        _ctx.Persons.Remove(entity);
        await _ctx.SaveChangesAsync();
    }
}
