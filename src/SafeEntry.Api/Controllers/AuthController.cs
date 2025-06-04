using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeEntry.Application.UseCases.ListUsers;
using SafeEntry.Application.UseCases.Login;
using SafeEntry.Application.UseCases.Register;  
using SafeEntry.Contracts.Request;             
using SafeEntry.Contracts.Response;           


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginHandler _handler;
    private readonly RegisterHandler _register;
    private readonly ListUsersHandler _listUsers;

    public AuthController(
       RegisterHandler register,
       LoginHandler login,
       ListUsersHandler listUsers)
    {
        _register = register;
        _handler = login;
        _listUsers = listUsers;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
    {
        try
        {
            var resp = await _handler.Handle(req);
            return Ok(resp);
        }
        catch (ApplicationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest req)
    {
        try
        {
            var resp = await _register.Handle(req);
            return CreatedAtAction(nameof(Register), new { userId = resp.UserId, personId = resp.PersonId }, resp);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
     // GET: api/auth/users
   [HttpGet("users")]
   public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
   {
      var list = await _listUsers.Handle();
        return Ok(list);
   }
}

