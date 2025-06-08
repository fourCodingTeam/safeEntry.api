using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories;

public class VisitorRepository : IVisitorRepository
{
    private readonly AppDbContext _context;

    public VisitorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Visitor> AddAsync(Visitor visitor)
    {
        _context.Visitors.Add(visitor);
        await _context.SaveChangesAsync();
        return visitor;
    }

    public async Task<Visitor?> GetByNameAndPhoneAsync(string name, long phoneNumber)
    {
        return await _context.Visitors.FirstOrDefaultAsync(v => v.Name == name && v.PhoneNumber == phoneNumber);
    }

    public async Task<Visitor> GetOrCreateAsync(string name, long phoneNumber)
    {
        var existing = await GetByNameAndPhoneAsync(name, phoneNumber);

        if (existing != null)
            return existing;

        var newVisitor = new Visitor(name, phoneNumber);
        return await AddAsync(newVisitor);
    }
}
