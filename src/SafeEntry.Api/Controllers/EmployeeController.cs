using Microsoft.AspNetCore.Mvc;
using SafeEntry.Application.Interfaces;
using SafeEntry.Contracts.Request;

namespace SafeEntry.Api.Controllers;

/// <summary>
/// Controller responsible for employee-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeController"/> class.
    /// </summary>
    /// <param name="employeeService">
    /// The service responsible for employee-related business operations.
    /// </param>
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="request">The data required to create the employee.</param>
    /// <returns>The created employee.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        try
        {
            var employee = await _employeeService.AddAsync(request);

            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
