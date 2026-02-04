using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Shared.DTO;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.Core.Features.Authentication.Login;

public class Login : ILogin
{
    private IUserRepository _userRepository;
    private ILogger<Login> _logger;
    private IJWTService _jwtService;

    public Login(IUserRepository userRepository, ILogger<Login> logger, IJWTService jwtService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }


    public async Task<ErrorOr<string>> InvokeAsync(LoginDTO request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request {@request}", request);

        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
        if (user == null) return Error.Unauthorized("Invalid email or passworad");
        
        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Error.Unauthorized("Invalid email or passworad");

        var token = _jwtService.GenerateToken(user.Id, user.Email);
        return token;
    }
}