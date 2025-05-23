namespace SafeEntry.Contracts.Request;

public class GenerateInviteRequest
{
    public int ResidentId { get; set; }
    public string VisitorName { get; set; } = string.Empty;
    public long VisitorPhoneNumber { get; set; }
    public DateTime StartDate { get; set; }
    public int DaysToExpiration { get; set; }
    public string Justification { get; set; } = string.Empty;
}