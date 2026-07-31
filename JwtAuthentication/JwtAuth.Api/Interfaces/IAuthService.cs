using JwtAuth.Api.DTOs;

namespace JwtAuth.Api.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequestDto request);

    Task<string> LoginAsync(LoginRequestDto request);
}