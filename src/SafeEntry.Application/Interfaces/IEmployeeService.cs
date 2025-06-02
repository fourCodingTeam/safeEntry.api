using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;

namespace SafeEntry.Application.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponse> AddAsync(CreateEmployeeRequest request);
}