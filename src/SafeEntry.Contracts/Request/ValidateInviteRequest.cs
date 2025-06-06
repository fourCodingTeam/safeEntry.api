namespace SafeEntry.Contracts.Request;

public class ValidateInviteRequest
{
    public int AddressId { get; set; }
    public int VisitorId { get; set; }
    public int Code { get; set; }
    public DateTime DateNow { get; protected set; }

    public ValidateInviteRequest(int addressId, int visitorId, int code) 
    {
        AddressId = addressId;
        VisitorId = visitorId;
        Code = code;
    }
}
