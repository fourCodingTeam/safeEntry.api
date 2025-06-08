namespace SafeEntry.Contracts.Request;

public class ValidateInviteRequest
{
    public int AddressId { get; set; }
    public int VisitorId { get; set; }
    public int EmployeeId { get; set; }
    public int Code { get; set; }
    public DateTime DateNow { get; set; }

    public ValidateInviteRequest(int addressId, int visitorId, int employeeId, int code, DateTime dateNow) 
    {
        AddressId = addressId;
        VisitorId = visitorId;
        EmployeeId = employeeId;
        Code = code;
        DateNow = dateNow;
    }
}
