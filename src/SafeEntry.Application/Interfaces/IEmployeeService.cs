using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponse> AddAsync(CreateEmployeeRequest request);
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
}