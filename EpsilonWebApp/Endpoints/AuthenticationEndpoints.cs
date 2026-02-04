using EpsilonWebApp.Core.Features.Authentication.Login;
using EpsilonWebApp.Shared.DTO;
using ErrorOr;
using MiniValidation;

namespace EpsilonWebApp.Endpoints;

public static class AuthenticationEndpoints
{
    
     public static IEndpointRouteBuilder RegisterAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/v1/auth")
            .WithTags("Auth")
            .DisableAntiforgery();

        
        group.MapPost("/login", async (HttpContext httpContext, ILogin login, LoginDTO request, CancellationToken cancellationToken) =>
        {
           if (!MiniValidator.TryValidate(request, out var errors))                                                                                                                                                                     
                return Results.ValidationProblem(errors);                                                                                                                                                                                  
       
            var response = await login.InvokeAsync(request, cancellationToken);
            if (!response.IsError)
            {
                httpContext.Response.Cookies.Append("X-AUTH-TOKEN", response.Value, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, 
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60),
                    Path = "/"
                });
            }
            
            return response.ToResult();
        }).AllowAnonymous();
        
       
        
       


        return app;
    }
}