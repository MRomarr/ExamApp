using Hangfire;

namespace ExamApp.Infrastructure.Configurations
{
    public static class HangfireJobsConfigurator
    {
        public static void ConfigureRecurringJobs()
        {
            RecurringJob.AddOrUpdate<IRefreshTokenCleanupJob>(
                "refresh-token-cleanup",
                job => job.RunAsync(CancellationToken.None),
                Cron.Daily(2)); // Run at 2 AM daily instead of every minute
        }
    }
}
