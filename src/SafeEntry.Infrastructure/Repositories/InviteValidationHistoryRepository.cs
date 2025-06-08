using MongoDB.Driver;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Infrastructure.Data;
using SafeEntry.Infrastructure.Models;

namespace SafeEntry.Infrastructure.Repositories;

public class InviteValidationHistoryRepository : IInviteValidationHistoryRepository
{
    private readonly IMongoCollection<InviteValidationHistoryMongoDbModel> _inviteHistory;

    public InviteValidationHistoryRepository(MongoDbContext context)
    {
        _inviteHistory = context.InvitesValidateHistory;
    }

    public async Task AddAsync(InviteValidationHistory inviteHistory)
    {
        var mongoInviteHistory = InviteValidationHistoryMongoDbModel.FromDomain(inviteHistory);

        await _inviteHistory.InsertOneAsync(mongoInviteHistory);
    }

    public async Task<IEnumerable<InviteValidationHistory>> GetAllAsync(int condominiumId)
    {
        return (await _inviteHistory
            .Find(x => x.CondominiumId == condominiumId)
            .SortByDescending(x => x.ValidatedAt)
            .ToListAsync())
            .Select(history => history.ToDomain());
    }

    public async Task<IEnumerable<InviteValidationHistory>> GetLastSevenDaysAsync(int condominiumId)
    {
        var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

        var filter = Builders<InviteValidationHistoryMongoDbModel>.Filter.And(
            Builders<InviteValidationHistoryMongoDbModel>.Filter.Eq(x => x.CondominiumId, condominiumId),
            Builders<InviteValidationHistoryMongoDbModel>.Filter.Gt(x => x.ValidatedAt, sevenDaysAgo));

        return (await _inviteHistory
            .Find(filter)
            .SortByDescending(x => x.ValidatedAt)
            .ToListAsync())
            .Select(history => history.ToDomain());
    }
}