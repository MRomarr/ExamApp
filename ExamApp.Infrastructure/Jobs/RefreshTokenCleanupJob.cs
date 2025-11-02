namespace ExamApp.Infrastructure.Jobs
{
    internal class RefreshTokenCleanupJob(ApplicationDbContext context)
    {

        public async Task RunAsync()
        {
            var tokens = await context.RefreshTokens
                .Where(x => x.IsRevoked || x.IsUsed || x.ExpiresOn <= DateTime.UtcNow)
                .ExecuteDeleteAsync();

            await context.SaveChangesAsync();
        }
    }
}
