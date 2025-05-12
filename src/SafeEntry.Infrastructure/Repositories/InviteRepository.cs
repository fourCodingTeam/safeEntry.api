using MongoDB.Driver;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;

namespace SafeEntry.Infrastructure.Repositories
{
    public class InviteRepository : IInviteRepository
    {
        private readonly IMongoCollection<Invite> _invites;

        public InviteRepository(MongoDbContext context)
        {
            _invites = context.Invites;
        }

        public async Task AddAsync(Invite invite)
        {
            await _invites.InsertOneAsync(invite);
        }

        public async Task<bool> ExistsCodeForResidentAsync(int residentId, int code)
        {
            var filter = Builders<Invite>.Filter.And(
                Builders<Invite>.Filter.Eq(x => x.ResidentId, residentId),
                Builders<Invite>.Filter.Eq(x => x.Code, code)
            );

            return await _invites.Find(filter).AnyAsync();
        }

        public async Task<bool> ValidateCodeAsync(int residentId, int visitorId, int code)
        {
            var filter = Builders<Invite>.Filter.And(
                Builders<Invite>.Filter.Eq(x => x.ResidentId, residentId),
                Builders<Invite>.Filter.Eq(x => x.VisitorId, visitorId),
                Builders<Invite>.Filter.Eq(x => x.Code, code)
            );

            var invite = await _invites.Find(filter).FirstOrDefaultAsync();

            return invite != null && invite.ExpirationDate > DateTime.UtcNow;
        }
    }
}
