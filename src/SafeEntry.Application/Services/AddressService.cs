using SafeEntry.Application.Interfaces;
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

    public async Task<IEnumerable<Address>> GetAddressesByEmployeeId(int employeeId)
    {
        var employee = await _employeeRepository.GetEmployeeById(employeeId);

        if (employee == null)
            throw new Exception("Employee not found");

        return await _addressRepository.GetByCondominiumId(employee.Condominium.Id);
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
}