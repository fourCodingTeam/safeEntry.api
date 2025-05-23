using MongoDB.Driver;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;
using SafeEntry.Infrastructure.Models;

namespace SafeEntry.Infrastructure.Repositories;

public class InviteRepository : IInviteRepository
{
    private readonly IMongoCollection<InviteMongoDbModel> _invites;

    public InviteRepository(MongoDbContext context)
    {
        _invites = context.Invites;
    }

    public async Task AddAsync(Invite invite)
    {
        var mongoInvite = InviteMongoDbModel.FromDomain(invite);

        await _invites.InsertOneAsync(mongoInvite);
    }

    public async Task<bool> ExistsCodeForResidentAsync(int residentId, int code)
    {
        var filter = Builders<InviteMongoDbModel>.Filter.And(
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.ResidentId, residentId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.Code, code)
        );

        return await _invites.Find(filter).AnyAsync();
    }

    public async Task<bool> ValidateCodeAsync(int residentId, int visitorId, int code)
    {
        var filter = BuildInviteFilter(residentId, visitorId, code);

        var invite = await _invites.Find(filter).FirstOrDefaultAsync();

        return invite != null && invite.ExpirationDate > DateTime.UtcNow;
    }

    public async Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId)
    {
        return (await _invites.Find(x => x.ResidentId == residentId).ToListAsync())
            .Select(mongoInvite => mongoInvite.ToDomain())
            .ToList();
    }

    public async Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(int residentId, int visitorId, int code)
    {
        var filter = BuildInviteFilter(residentId, visitorId, code);

        var mongoInvite = await _invites.Find(filter).FirstOrDefaultAsync();

        if (mongoInvite == null)
            throw new Exception("Invite not found");

        return mongoInvite.ToDomain();
    }

    private FilterDefinition<InviteMongoDbModel> BuildInviteFilter(int residentId, int visitorId, int code)
    {
        return Builders<InviteMongoDbModel>.Filter.And(
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.ResidentId, residentId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.VisitorId, visitorId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.Code, code)
        );
    }
}

