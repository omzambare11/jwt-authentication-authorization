using JwtAuth.Api.DTOs;
using JwtAuth.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var result = await _authService.RegisterAsync(request);

        if (result == "Email already exists.")
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}