namespace ExamApp.Application.Features.Auth.RefreshToken
{
    internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthDto>>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenCommandHandler(ITokenProvider tokenProvider, IRefreshTokenRepository refreshTokenRepository, ILogger<RefreshTokenCommandHandler> logger)
        {
            _tokenProvider = tokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Result<AuthDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (storedRefreshToken == null || !storedRefreshToken.IsActive)
                return Result<AuthDto>.Failure("Invalid refresh token");

            storedRefreshToken.IsUsed = true;
            await _refreshTokenRepository.SaveChangesAsync();

            var accessToken = await _tokenProvider.GenerateAccessTokenAsync(storedRefreshToken.User);
            var refreshToken = await _tokenProvider.GenerateAndStoreRefreshTokenAsync(storedRefreshToken.User);

            var authResult = new AuthDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return (Result<AuthDto>.Success(authResult));
        }
    }

}