using System.Security.Claims;

namespace EpsilonWebApp.Core.Contracts;

public interface IJWTService
{
    string GenerateToken(Guid userId, string email);
    ClaimsPrincipal? ValidateToken(string token);
}