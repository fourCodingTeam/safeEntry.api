namespace SafeEntry.Contracts.Request
{
    public class GenerateInviteRequest
    {
        public int ResidenteId { get; set; }
        public int VisitorId { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysToExpiration { get; set; }
        public string Justification { get; set; } = string.Empty;
    }
}