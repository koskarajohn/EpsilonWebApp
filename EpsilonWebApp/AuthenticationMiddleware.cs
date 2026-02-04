using EpsilonWebApp.Core.Contracts;

namespace EpsilonWebApp;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }
    
    public async Task InvokeAsync(HttpContext context, IJWTService jwtService)
    {
        
        var toBeAuthenticated = !context.Request.Path.Equals("/api/v1/auth/login", StringComparison.OrdinalIgnoreCase) &&
                                     ( context.Request.Path == "/" || context.Request.Path.StartsWithSegments("/api/v1", StringComparison.OrdinalIgnoreCase));

        if (toBeAuthenticated)
        {
            if (!context.Request.Cookies.TryGetValue("X-AUTH-TOKEN", out var cookie))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            
            var principal = jwtService.ValidateToken(cookie);
            if (principal is null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            else
            {
                context.User = principal;
            }
        }

        await _next(context);
    }
}