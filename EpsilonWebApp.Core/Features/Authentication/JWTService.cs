using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EpsilonWebApp.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EpsilonWebApp.Core.Features.Authentication;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }


    public string GenerateToken(Guid userId, string email)
    {
        var key = _configuration["JwtSettings:SecretKey"];
        var duration = _configuration["JwtSettings:Duration"];
        
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(symmetricKey ,SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim("id", userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };
        
        var token = new JwtSecurityToken(
            issuer: "epsilon",
            audience: "audience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(duration)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}