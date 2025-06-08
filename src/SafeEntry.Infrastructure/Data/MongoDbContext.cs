using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SafeEntry.Infrastructure.Models;

namespace SafeEntry.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
        _database = client.GetDatabase("SafeEntry");
    }

        public IMongoCollection<InviteMongoDbModel> Invites => _database.GetCollection<InviteMongoDbModel>("Invites");
        public IMongoCollection<InviteValidationHistoryMongoDbModel> InvitesValidateHistory => _database.GetCollection<InviteValidationHistoryMongoDbModel>("InvitesValidateHistory");
}