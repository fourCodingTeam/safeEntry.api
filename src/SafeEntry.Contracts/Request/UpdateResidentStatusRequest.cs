namespace SafeEntry.Contracts.Request
{
    using SafeEntry.Domain.Enum;

    public record UpdateResidentStatusRequest(StatusResident NewStatus);
}
