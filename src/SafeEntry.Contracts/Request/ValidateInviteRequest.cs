namespace SafeEntry.Contracts.Request;

public class ValidateInviteRequest
{
    public ValidateInviteRequest(int residentId, int visitorId, int code) 
    {
        ResidentId = residentId;
        VisitorId = visitorId;
        Code = code;
    }

    public int ResidentId { get; set; }
    public int VisitorId { get; set; }
    public int Code { get; set; }
}
