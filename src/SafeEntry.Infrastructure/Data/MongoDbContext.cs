using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            _database = client.GetDatabase("SafeEntry");
        }

        public IMongoCollection<Invite> Invites => _database.GetCollection<Invite>("Invites");
    }
}