namespace SafeEntry.Domain.Entities;

public class Invite
{
    public int Code { get; protected set; }
    public int ResidentId { get; protected set; }
    public int AddressId { get; protected set; }
    public int VisitorId {  get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime StartDate { get; protected set; }
    public DateTime ExpirationDate { get; protected set; }
    public string? Justification { get; protected set; } = null!;
    public bool IsActive { get; protected set; }

    protected Invite() { }

    public Invite(int code, int residentId, int addressId, int visitorId, DateTime start, DateTime expiration, string justification)
    {
        Code = code;
        ResidentId = residentId;
        AddressId = addressId;
        VisitorId = visitorId;
        CreatedAt = DateTime.UtcNow;
        StartDate = start;
        ExpirationDate = expiration;
        Justification = justification;
        IsActive = true;
    }
}
