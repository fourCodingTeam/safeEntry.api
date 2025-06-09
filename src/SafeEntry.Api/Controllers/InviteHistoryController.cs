using Microsoft.AspNetCore.Mvc;
using SafeEntry.Application.Interfaces;

namespace SafeEntry.Api.Controllers;

/// <summary>
/// Controller responsible for invite validation history operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InviteHistoryController : ControllerBase
{
    private readonly IInviteValidationHistoryService _historyService;

    /// <summary>
    /// Initializes a new instance of the InviteHistoryController class.
    /// </summary>
    /// <param name="historyService">
    /// The service responsible for invite validation history operations.
    /// </param>
    public InviteHistoryController(IInviteValidationHistoryService historyService)
    {
        _historyService = historyService;
    }

    /// <summary>
    /// Gets all invite validation history records for a condominium.
    /// </summary>
    /// <param name="condominiumId">The unique identifier of the condominium.</param>
    /// <returns>
    /// 200 OK with a list of invite validation history records if found;
    /// 404 Not Found if no records exist.
    /// </returns>
    [HttpGet("condominium/{condominiumId}")]
    public async Task<IActionResult> GetAll([FromRoute] int condominiumId)
    {
        var invites = await _historyService.GetAllAsync(condominiumId);

        if (invites == null || !invites.Any())
            return NotFound("No invites found for the condominium");

        return Ok(invites);
    }

    /// <summary>
    /// Gets invite validation history records for the last seven days for a condominium.
    /// </summary>
    /// <param name="condominiumId">The unique identifier of the condominium.</param>
    /// <returns>
    /// 200 OK with a list of invite validation history records from the last seven days if found;
    /// 404 Not Found if no records exist.
    /// </returns>
    [HttpGet("condominium/lastweek/{condominiumId}")]
    public async Task<IActionResult> GetLastSevenDays([FromRoute] int condominiumId)
    {
        var invites = await _historyService.GetLastSevenDaysAsync(condominiumId);

        if (invites == null || !invites.Any())
            return NotFound("No invites found for the condominium");

        return Ok(invites);
    }

    /// <summary>
    /// Gets a specific invite validation history record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the invite validation history record.</param>
    /// <returns>
    /// 200 OK with the invite validation history record if found;
    /// 404 Not Found if the record does not exist.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInviteHistoryById([FromRoute] string id)
    {
        var invite = await _historyService.GetInviteHistorybyIdAsync(id);

        if (invite == null)
            return NotFound("No invite history found for the provided ID");

        return Ok(invite);
    }
}
