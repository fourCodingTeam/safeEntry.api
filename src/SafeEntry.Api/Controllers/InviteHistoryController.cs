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
    /// Initializes a new instance of the <see cref="InviteHistoryController"/> class.
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
    /// <param name="condominiumId">The condominium's ID.</param>
    /// <returns>A list of invite validation history records for the condominium.</returns>
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
    /// <param name="condominiumId">The condominium's ID.</param>
    /// <returns>A list of invite validation history records for the last seven days.</returns>
    [HttpGet("condominium/lastweek/{condominiumId}")]
    public async Task<IActionResult> GetLastSevenDays([FromRoute] int condominiumId)
    {
        var invites = await _historyService.GetLastSevenDaysAsync(condominiumId);

        if (invites == null || !invites.Any())
            return NotFound("No invites found for the condominium");

        return Ok(invites);
    }
}
