using EpsilonWebApp.Shared.DTO;
using ErrorOr;

namespace EpsilonWebApp.Core.Features.Authentication.Login;

public interface ILogin
{
    Task<ErrorOr<string>> InvokeAsync(LoginDTO request, CancellationToken cancellationToken);
}