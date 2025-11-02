
namespace ExamApp.Application.Features.Auth.Logout
{
    internal class LogoutCommandHandelr : IRequestHandler<LogoutCommand, Result>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserContext _userContext;

        public LogoutCommandHandelr(IRefreshTokenRepository refreshTokenRepository, IUserContext userContext)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userContext = userContext;
        }
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            if (userId == null)
                return Result.Failure("You are not authenticated");

            var tokens = await _refreshTokenRepository.GetByUserIdAsync(userId);

            foreach (var token in tokens)
                token.IsRevoked = true;

            await _refreshTokenRepository.SaveChangesAsync();

            return Result.Success();

        }
    }

}
