namespace JwtAuth.Api.Interfaces;

public interface IJwtService
{
    string GenerateToken(Entities.User user);
}