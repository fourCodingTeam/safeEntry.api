namespace SafeEntry.Contracts.Requests;

public class CreateAddressRequest
{
    public string Street { get; set; } = null!;
    public int Number { get; set; }
    public string Neighborhood { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Country { get; set; } = null!;
}
