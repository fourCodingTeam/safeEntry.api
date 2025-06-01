using Microsoft.AspNetCore.Mvc;
using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Services;

namespace SafeEntry.Api.Controllers;

/// <summary>
/// Controller responsible for invite-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InviteController : ControllerBase
{
    private readonly IInviteService _inviteService;

    public InviteController(IInviteService inviteService)
    {
        _inviteService = inviteService;
    }

    /// <summary>
    /// Generates a new invite code for a visitor.
    /// </summary>
    /// <param name="request">The data required to generate the invite.</param>
    /// <returns>The generated invite code.</returns>
    [HttpPost("Generate")]
    public async Task<IActionResult> GenerateInvite([FromBody] GenerateInviteRequest request)
    {
        try
        {
            var code = await _inviteService.GenerateCodeAsync(request);

            return Ok(code);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    /// <summary>
    /// Validates an invite code.
    /// </summary>
    /// <param name="request">The data required to validate the invite code.</param>
    /// <returns>A message indicating whether the code is valid or not.</returns>
    [HttpPost("Validate")]
    public async Task<IActionResult> ValidateInvite([FromBody] ValidateInviteRequest request)
    {
        var isValid = await _inviteService.ValidateCodeAsync(request);

        if (!isValid)
            return BadRequest("Invalid or expired code");

        return Ok("Code is valid");
    }

    /// <summary>
    /// Gets all invites associated with a resident.
    /// </summary>
    /// <param name="residentId">The resident's ID.</param>
    /// <returns>A list of invites for the resident.</returns>
    [HttpGet("{residentId}/invites")]
    public async Task<IActionResult> GetInvitesByResidentId([FromRoute] int residentId)
    {
        var invites = await _inviteService.GetInvitesByResidentIdAsync(residentId);

        if (invites == null || !invites.Any())
            return NotFound("No invites found for the resident");

        return Ok(invites);
    }

    /// <summary>
    /// Gets a specific invite by resident ID, visitor ID, and code.
    /// </summary>
    /// <param name="residentId">The resident's ID.</param>
    /// <param name="visitorId">The visitor's ID.</param>
    /// <param name="code">The invite code.</param>
    /// <returns>The invite if found, otherwise a not found message.</returns>
    [HttpGet("{residentId}/{visitorId}/{code}")]
    public async Task<IActionResult> GetInvitesByResidentIdAndVisitorId([FromRoute] int residentId, int visitorId, int code)
    {
        var invite = await _inviteService.GetInviteByResidentIdAndVisitorIdAsync(residentId, visitorId, code);

        if (invite == null)
            return NotFound("Invite Not Found");

        return Ok(invite);
    }

}