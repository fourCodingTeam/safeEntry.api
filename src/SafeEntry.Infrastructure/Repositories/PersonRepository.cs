using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _ctx;
        public PersonRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<Person?> GetByIdAsync(int personId)
        {
            return await _ctx.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        }
    }
}