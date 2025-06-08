using SafeEntry.Application.Interfaces;
using SafeEntry.Application.UseCases.Register;
using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;
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

    public async Task<EmployeeResponse> AddAsync(CreateEmployeeRequest request)
    {
        var condominium = await _condominiumRepository.GetCondominiumByIdAsync(request.CondominiumId);

        if (condominium == null)
            throw new Exception("Condominium not found");
            
        var employee = new Employee(request.Name, request.PhoneNumber, request.Position, condominium);
        await _employeeRepository.AddAsync(employee);

        var user = new RegisterRequest(employee.Id, request.Email, request.Password, UserTypeEnum.Employee);
        await _registerHandler.Handle(user);

        return new EmployeeResponse(employee.Id, employee.Name, employee.Position, condominium.Name);
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    { 
        var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

        if (employee == null)
            throw new Exception("Employee not found");

        return employee;
    }

}