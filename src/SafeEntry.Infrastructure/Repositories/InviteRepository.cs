using Microsoft.EntityFrameworkCore;
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

    private FilterDefinition<InviteMongoDbModel> BuildInviteFilter(int addressId, int visitorId, int code)
    {
        return Builders<InviteMongoDbModel>.Filter.And(
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.AddressId, addressId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.VisitorId, visitorId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.Code, code)
        );
    }

    public async Task<bool> ExistsCodeForAddressAsync(int addressId, int code)
    {
        var filter = Builders<InviteMongoDbModel>.Filter.And(
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.AddressId, addressId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.Code, code)
        );

        return await _invites.Find(filter).AnyAsync();
    }

    public async Task<bool> ValidateCodeAsync(int addressId, int visitorId, int code, DateTime dateNow)
    {
        var filter = BuildInviteFilter(addressId, visitorId, code);

        var invite = await _invites.Find(filter).FirstOrDefaultAsync();

        if (invite == null)
            throw new Exception("Invite not found");



        return invite.IsActive && invite.ExpirationDate.Date >= dateNow.Date;
    }

    public async Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId)
    {
        return (await _invites.Find(x => x.ResidentId == residentId).ToListAsync())
            .Select(mongoInvite => mongoInvite.ToDomain());
    }

    public async Task<IEnumerable<Invite>> GetInvitesByAddressIdAsync(int addressId)
    {
        return (await _invites.Find(x => x.AddressId == addressId).ToListAsync())
            .Select(mongoInvite => mongoInvite.ToDomain());
    }

    public async Task<Invite> GetInviteByAddressIdAndVisitorIdAsync(int addressId, int visitorId, int code)
    {
        var filter = BuildInviteFilter(addressId, visitorId, code);

        var mongoInvite = await _invites.Find(filter).FirstOrDefaultAsync();

        if (mongoInvite == null)
            throw new KeyNotFoundException("Invite not found");

        return mongoInvite.ToDomain();
    }

    public async Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(int residentId, int visitorId, int code)
    {
        var filter = Builders<InviteMongoDbModel>.Filter.And(
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.ResidentId, residentId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.VisitorId, visitorId),
            Builders<InviteMongoDbModel>.Filter.Eq(x => x.Code, code)
        );

        var mongoInvite = await _invites.Find(filter).FirstOrDefaultAsync();

        if (mongoInvite == null)
            throw new KeyNotFoundException("Invite not found");

        return mongoInvite.ToDomain();
    }

    public async Task<long> CountByAddressIdAsync(int addressId)
    {
        return await _invites.CountDocumentsAsync(x => x.AddressId == addressId);
    }

    public async Task<bool> ActivateInviteAsync(int addressId, int visitorId, int code)
    {
        var filter = BuildInviteFilter(addressId, visitorId, code);

        var update = Builders<InviteMongoDbModel>.Update.Set(i => i.IsActive, true);

        var result = await _invites.UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeactivateInviteAsync(int addressId, int visitorId, int code)
    {
        var filter = BuildInviteFilter(addressId, visitorId, code);

        var update = Builders<InviteMongoDbModel>.Update.Set(i => i.IsActive, false);

        var result = await _invites.UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }
}

