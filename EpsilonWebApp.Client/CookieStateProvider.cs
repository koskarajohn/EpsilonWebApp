using System.Security.Claims;
using EpsilonWebApp.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;

namespace EpsilonWebApp.Client;

public class CookieStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private APIClient _client;

    public CookieStateProvider(APIClient client)
    {
        _client = client;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userInfo = await _client.GetAsync<UserInfo>("auth/me");
            
            var claims = new[]
            {
                new Claim("id", userInfo.Id.ToString()),
                new Claim(ClaimTypes.Email, userInfo.Email)
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return new AuthenticationState(claimsPrincipal);
        }
        catch (Exception e)
        {
            return new AuthenticationState(_anonymous);
        }
    }
    
    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}