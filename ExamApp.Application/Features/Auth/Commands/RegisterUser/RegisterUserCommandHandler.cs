using Microsoft.AspNetCore.Identity;

namespace ExamApp.Application.Features.Auth.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager, ITokenProvider tokenProvider, ILogger<RegisterUserCommandHandler> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _logger = logger;
        }


        public async Task<Result<AuthDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registering new user with username: {UserName} and email: {Email}", request.UserName, request.Email);
            _logger.LogInformation("Handling RegisterUserCommand for user: {UserName}", request.UserName);

            if (await _userManager.FindByNameAsync(request.UserName) != null)
                return Result<AuthDto>.Failure("Username is already taken.");

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return Result<AuthDto>.Failure("Email is already registered.");

            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Result<AuthDto>.Failure(result.Errors.First().Description);

            await _userManager.AddToRoleAsync(user, Role.User);
            var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenProvider.GenerateAndStoreRefreshTokenAsync(user);

            return Result<AuthDto>.Success(new AuthDto { AccessToken = accessToken, RefreshToken = refreshToken });
        }
    }
}
