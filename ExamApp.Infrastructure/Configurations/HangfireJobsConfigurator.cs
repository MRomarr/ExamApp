using ExamApp.Infrastructure.Jobs;
using Hangfire;

namespace ExamApp.Infrastructure.Configurations
{
    public static class HangfireJobsConfigurator
    {
        public static void ConfigureRecurringJobs()
        {
            RecurringJob.AddOrUpdate<RefreshTokenCleanupJob>(
                "refresh-token-cleanup",
                job => job.RunAsync(),
                Cron.Minutely);
        }
    }
}
