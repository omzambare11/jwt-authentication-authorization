using BCrypt.Net;
using JwtAuth.Api.Data;
using JwtAuth.Api.DTOs;
using JwtAuth.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (existingUser != null)
        {
            return BadRequest("Email already exists.");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User"
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }
}