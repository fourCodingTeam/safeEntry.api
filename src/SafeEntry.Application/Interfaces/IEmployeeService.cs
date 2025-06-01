using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;

namespace SafeEntry.Application.Interfaces;

public interface IEmployeeService
{
    Task<Employee> AddAsync(CreateEmployeeRequest request);
}