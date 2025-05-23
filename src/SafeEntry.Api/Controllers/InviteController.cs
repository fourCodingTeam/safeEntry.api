using Microsoft.AspNetCore.Mvc;
using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Services;

namespace SafeEntry.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InviteController : ControllerBase
{
    private readonly IInviteService _inviteService;

    public InviteController(IInviteService inviteService)
    {
        _inviteService = inviteService;
    }

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

    [HttpPost("Validate")]
    public async Task<IActionResult> ValidateInvite([FromBody] ValidateInviteRequest request)
    {
        var isValid = await _inviteService.ValidateCodeAsync(request);

        if (!isValid)
            return BadRequest("Invalid or expired code");

        return Ok("Code is valid");
    }

    [HttpGet("{residentId}/invites")]
    public async Task<IActionResult> GetInvitesByResidentId([FromRoute] int residentId)
    {
        var invites = await _inviteService.GetInvitesByResidentIdAsync(residentId);

        if (invites == null || !invites.Any())
            return NotFound("No invites found for the resident");

        return Ok(invites);
    }

    [HttpGet("{residentId}/{visitorId}/{code}")]
    public async Task<IActionResult> GetInvitesByResidentIdAndVisitorId([FromRoute] ValidateInviteRequest request)
    {
        var invite = await _inviteService.GetInviteByResidentIdAndVisitorIdAsync(request);

        if (invite == null)
            return NotFound("Invite Not Found");

        return Ok(invite);
    }

}