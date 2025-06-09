using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Infrastructure.Models;

public class InviteMongoDbModel
{
    [BsonId]
    public ObjectId MongoId { get; set; }

    public int Code { get; set; }
    public int ResidentId { get; set; }
    public int AddressId { get; set; }
    public int VisitorId { get; set; }
    public string VisitorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int DaysToExpiration { get; set; }
    public string? Justification { get; set; }
    public bool IsActive { get; set; }

    public static InviteMongoDbModel FromDomain(Invite invite)
    {
        return new InviteMongoDbModel
        {
            Code = invite.Code,
            ResidentId = invite.ResidentId,
            VisitorId = invite.VisitorId,
            VisitorName = invite.VisitorName,
            CreatedAt = invite.CreatedAt,
            StartDate = invite.StartDate,
            ExpirationDate = invite.ExpirationDate,
            DaysToExpiration = invite.DaysToExpiration,
            Justification = invite.Justification,
            IsActive = invite.IsActive,
            AddressId = invite.AddressId
        };
    }

    public Invite ToDomain()
    {
        return new Invite(Code, ResidentId, AddressId, VisitorId, VisitorName, StartDate, ExpirationDate, DaysToExpiration, Justification ?? string.Empty, IsActive);
    }
}

