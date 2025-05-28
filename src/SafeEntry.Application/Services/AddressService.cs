using SafeEntry.Application.Interfaces;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AddressService(IAddressRepository addressRepository, IEmployeeRepository employeeRepository)
    {
        _addressRepository = addressRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Address>> GetAddressesByEmployeeId(int employeeId)
    {
        var employee = await _employeeRepository.GetEmployeeById(employeeId);

        if (employee == null)
            throw new Exception("Employee not found");

        return await _addressRepository.GetByCondominiumId(employee.Condominium.Id);
    }
}