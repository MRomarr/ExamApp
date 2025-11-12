using Microsoft.Extensions.Logging;

namespace ExamApp.Infrastructure.Jobs
{


    internal class RefreshTokenCleanupJob : IRefreshTokenCleanupJob
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RefreshTokenCleanupJob> _logger;

        public RefreshTokenCleanupJob(ApplicationDbContext context, ILogger<RefreshTokenCleanupJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {

            _logger.LogInformation("Starting refresh token cleanup job at {Time}", DateTime.UtcNow);

            var deletedCount = await _context.RefreshTokens
                .Where(x => x.IsRevoked || x.IsUsed || x.ExpiresOn <= DateTime.UtcNow)
                .ExecuteDeleteAsync(cancellationToken);

            _logger.LogInformation("Deleted {Count} expired/used refresh tokens", deletedCount);


        }
    }
}