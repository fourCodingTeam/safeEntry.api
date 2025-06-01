using Microsoft.EntityFrameworkCore;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<User?> GetByEmailAsync(string email)
            => _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }
        public Task<IEnumerable<User>> GetAllAsync() =>
          _ctx.Users
              .Include(u => u.Person)
              .AsNoTracking()
              .ToListAsync()
              .ContinueWith(t => (IEnumerable<User>)t.Result);

    }
}