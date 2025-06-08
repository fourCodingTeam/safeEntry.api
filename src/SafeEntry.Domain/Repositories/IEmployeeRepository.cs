using SafeEntry.Domain.Entities;

namespace SafeEntry.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> AddAsync(Employee employee);
    Task<Employee?> GetEmployeeByIdAsync(int id);
}