namespace SafeEntry.Contracts.Request;

public class InviteHistoryRequest : ValidateInviteRequest
{
    public bool Approval { get; set; }

    public InviteHistoryRequest(int addressId, int visitorId, int employeeId, int code, DateTime dateNow, bool approval) 
        : base(addressId, visitorId, employeeId, code, dateNow)
    {
        Approval = approval;
    }
}