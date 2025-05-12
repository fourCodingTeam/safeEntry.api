using Microsoft.AspNetCore.Mvc;
using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Services;

namespace SafeEntry.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateInvite([FromBody] GenerateInviteRequest request)
        {
            try
            {
                var code = await _inviteService.GenerateCodeAsync(
                    request.ResidenteId,
                    request.VisitorId,
                    request.StartDate,
                    request.DaysToExpiration,
                    request.Justification
                );

                return Ok(code);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateInvite([FromBody] ValidateInviteRequest request)
        {
                var isValid = await _inviteService.ValidateCodeAsync(
                    request.ResidenteId,
                    request.VisitorId,
                    request.Code
                );

                if (!isValid)
                    return BadRequest("Invalid or expired code");

            return Ok("Code is valid");
        }
    }
}
