using SafeEntry.Domain.Enum;

namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public Address Address { get; protected set; } = null!;

    public StatusResident StatusResident { get; set; }

    protected Resident() { }

    public Resident(string name, long phoneNumber, Address address, StatusResident statusResident)
        : base(name, phoneNumber)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
        StatusResident = StatusResident.Disponivel;
    }
    public void UpdateResidentStatus(StatusResident newStatus)
    {
        StatusResident = newStatus;
    }

}
