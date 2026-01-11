using Api.Dtos;
using Application.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _authenticationService.Register(dto.Username, dto.Email, dto.Password);
        if (user == null)
        {
            return BadRequest("User with this username or email already exists.");
        }

        return Ok(new { user.Username, user.Email, Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _authenticationService.Login(dto.Email, dto.Password);
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var token = _authenticationService.CreateToken(user);
        return Ok(new { token, user.Username, user.Email });
    }
}
