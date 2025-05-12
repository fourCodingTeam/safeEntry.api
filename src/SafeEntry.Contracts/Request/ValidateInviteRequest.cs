namespace SafeEntry.Contracts.Request
{
    public class ValidateInviteRequest
    {
        public int ResidenteId { get; set; }
        public int VisitorId { get; set; }
        public int Code { get; set; }
    }
}