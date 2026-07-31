using BCrypt.Net;
using JwtAuth.Api.DTOs;
using JwtAuth.Api.Entities;
using JwtAuth.Api.Interfaces;

namespace JwtAuth.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userRepository
            .GetByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return "Email already exists.";
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User"
        };

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

        return "User registered successfully.";
    }

    public Task<string> LoginAsync(LoginRequestDto request)
    {
        throw new NotImplementedException();
    }
}