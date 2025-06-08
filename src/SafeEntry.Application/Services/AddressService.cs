using SafeEntry.Application.Interfaces;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICondominiumRepository _condominiumRepository;

    public AddressService(IAddressRepository addressRepository, IEmployeeRepository employeeRepository, ICondominiumRepository condominiumRepository)
    {
        _addressRepository = addressRepository;
        _employeeRepository = employeeRepository;
        _condominiumRepository = condominiumRepository;
    }

    public async Task<IEnumerable<AddressResponse>> GetAddressesByEmployeeId(int employeeId)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

        if (employee == null)
            throw new Exception("Employee not found");

        var addresses = await _addressRepository.GetByCondominiumId(employee.CondominiumId);

        return addresses.Select(address => new AddressResponse(
            address.Id,
            address.HomeStreet,
            address.HomeNumber,
            address.Condominium.Name,
            address.HouseOwnerId,
            address.Residents.Select(resident => new ResidentResponse(
                resident.Id,
                resident.Name,
                resident.PhoneNumber,
                resident.IsHomeOwner,
                resident.StatusResident
            )).ToList()
        ));
    }

    public async Task<Address> GetOrCreateAsync(int condominiumId, int homeNumber, string? homeStreet)
    {
        var existing = await _addressRepository.GetByCondominiumIdAndNumber(condominiumId, homeNumber);

        if (existing != null)
            return existing;

        var condominium = await _condominiumRepository.GetCondominiumByIdAsync(condominiumId);

        if(condominium == null)
            throw new ArgumentNullException("Condominium not Found");

        var newAddress = new Address(condominium, homeStreet, homeNumber);

        return await _addressRepository.AddAsync(newAddress);
    }

    public async Task<Address> GetByResidentIdAsync(int residentId)
    {
        var response = await _addressRepository.GetByResidentIdAsync(residentId);

        if (response == null)
            throw new ArgumentNullException("Condominium not Found");

        return response;

    }

    public async Task UpdateAsync(Address address)
    {
        await _addressRepository.UpdateAsync(address);
    }
}