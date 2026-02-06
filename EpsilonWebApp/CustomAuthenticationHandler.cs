using System.Text.Encodings.Web;
using EpsilonWebApp.Core.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace EpsilonWebApp;

public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IJWTService _jwtService;

    public CustomAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, 
        UrlEncoder encoder, IJWTService jwtService) : base(options, logger, encoder)
    {
        _jwtService = jwtService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        
        if (!Request.Cookies.TryGetValue("X-AUTH-TOKEN", out var token))
            return Task.FromResult(AuthenticateResult.NoResult());
        
        try
        {
            var principal = _jwtService.ValidateToken(token);
            if (principal == null)
                return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
            
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error validating authentication token");
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }

    }
    
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Context.Response.Redirect("/login");
        return Task.CompletedTask;
    }
    
}