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
        try
        {
            
            var key = _configuration["JwtSettings:SecretKey"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "epsilon",
                ValidAudience = "audience",
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
            return principal;
        }
        catch
        {
            return null;
        }
    }
    
}