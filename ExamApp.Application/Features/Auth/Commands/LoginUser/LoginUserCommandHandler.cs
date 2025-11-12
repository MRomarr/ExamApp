using Microsoft.AspNetCore.Identity;

namespace ExamApp.Application.Features.Auth.Commands.LoginUser
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly ITokenProvider _tokenProvider;
        public LoginUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<LoginUserCommandHandler> logger,
            ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<AuthDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling LoginUserCommand for {UserNameOrEmail}", request.UserNameOrEmail);
            var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user is null)
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user is null)
                return Result<AuthDto>.Failure("Invalid username or password.");
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                return Result<AuthDto>.Failure("Invalid username or password.");


            var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenProvider.GenerateAndStoreRefreshTokenAsync(user);

            var authDto = new AuthDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            _logger.LogInformation("User {UserNameOrEmail} logged in successfully", request.UserNameOrEmail);
            return Result<AuthDto>.Success(authDto, "Login successful.");
        }
    }
}
