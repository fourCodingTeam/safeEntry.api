using Microsoft.AspNetCore.Mvc;
using SafeEntry.Application.Interfaces;

namespace SafeEntry.Api.Controllers;

/// <summary>
/// Controller responsible for address-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressController"/> class.
    /// </summary>
    /// <param name="addressService">
    /// The service responsible for address-related business operations.
    /// </param>
    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    /// <summary>
    /// Gets all addresses associated with a specific employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee whose addresses will be retrieved.</param>
    /// <returns>A list of addresses for the specified employee.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAddressesByEmployeeId(int employeeId)
    {
        try
        {
            var addresses = await _addressService.GetAddressesByEmployeeId(employeeId);

            return Ok(addresses);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
