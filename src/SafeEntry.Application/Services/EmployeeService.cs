using SafeEntry.Application.Interfaces;
using SafeEntry.Application.UseCases.Register;
using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Enums;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICondominiumRepository _condominiumRepository;
    private readonly RegisterHandler _registerHandler;

    public EmployeeService(IEmployeeRepository employeeRepository, ICondominiumRepository condominiumRepository, RegisterHandler registerHandler)
    {
        _employeeRepository = employeeRepository;
        _condominiumRepository = condominiumRepository;
        _registerHandler = registerHandler;
    }

    public async Task<Employee> AddAsync(CreateEmployeeRequest request)
    {
        var condominium = await _condominiumRepository.GetCondominiumByIdAsync(request.CondominiumId);

        if (condominium == null)
            throw new Exception("Condominium not found");
            
        var employee = new Employee(request.Name, request.PhoneNumber, request.Position, condominium);

        var user = new RegisterRequest(employee.Id, request.Email, request.Password, UserTypeEnum.Employee);

        await _registerHandler.Handle(user);

        return employee;
    }
}