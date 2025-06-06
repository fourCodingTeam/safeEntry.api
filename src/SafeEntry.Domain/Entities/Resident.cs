using SafeEntry.Domain.Enum;

namespace SafeEntry.Domain.Entities;

public class Resident : Person
{
    public Address Address { get; protected set; } = null!;
    public int AddressId { get; protected set; }
    public bool IsHomeOwner { get; protected set; }
    public StatusResident StatusResident { get; set; }
    protected Resident() { }

    public Resident(string name, long phoneNumber, Address address, bool isHomeOwner)
        : base(name, phoneNumber)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
        AddressId = address.Id;
        IsHomeOwner = isHomeOwner;
        StatusResident = StatusResident.Disponivel;
    }

    public void SetAsHouseOwner(bool isOwner)
    {
        IsHomeOwner = isOwner;
    }
    public void UpdateResidentStatus(StatusResident newStatus)
    {
        StatusResident = newStatus;
    }
}
