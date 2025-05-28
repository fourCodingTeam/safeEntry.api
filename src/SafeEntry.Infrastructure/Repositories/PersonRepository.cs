using System.Threading.Tasks;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _ctx;
        public PersonRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Person person)
        {
            _ctx.Persons.Add(person);
            await _ctx.SaveChangesAsync();
        }
    }
}