using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Infrastructure.Models;

public class InviteValidationHistoryMongoDbModel
{
    [BsonId]
    public ObjectId MongoId { get; set; }

    public int CondominiumId { get; set; }
    public int AddressId { get; set; }
    public string HomeDescription { get; set; } = null!;
    public string CreatedByResidentName { get; set; } = null!;
    public int VisitorId { get; set; }
    public string VisitorName { get; set; } = null!;
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = null!;
    public int Code { get; set; }
    public DateTime InviteExpirationDate { get; set; }
    public DateTime ValidatedAt { get; set; }
    public bool Approval { get; set; }

    public static InviteValidationHistoryMongoDbModel FromDomain(InviteValidationHistory history)
    {
        return new InviteValidationHistoryMongoDbModel
        {
            AddressId = history.AddressId,
            CondominiumId = history.CondominiumId,
            HomeDescription = history.HomeDescription,
            CreatedByResidentName = history.CreatedByResidentName,
            VisitorId = history.VisitorId,
            VisitorName = history.VisitorName,
            EmployeeId = history.EmployeeId,
            EmployeeName = history.EmployeeName,
            Code = history.Code,
            InviteExpirationDate = history.InviteExpirationDate,
            ValidatedAt = history.ValidatedAt,
            Approval = history.Approval
        };
    }

    public InviteValidationHistory ToDomain()
    {
        return new InviteValidationHistory(
            AddressId,
            HomeDescription,
            CreatedByResidentName,
            VisitorId,
            VisitorName,
            EmployeeId,
            EmployeeName,
            Code,
            InviteExpirationDate,
            ValidatedAt,
            Approval
        );
    }
}