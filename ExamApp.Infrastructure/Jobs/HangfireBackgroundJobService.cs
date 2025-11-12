using Hangfire;
using System.Linq.Expressions;

namespace ExamApp.Infrastructure.Jobs
{
    public class HangfireBackgroundJobService : IBackgroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireBackgroundJobService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public string Schedule<TJob>(Expression<Func<TJob, Task>> methodCall, DateTimeOffset enqueueAt)
            where TJob : class
        {
            return _backgroundJobClient.Schedule(methodCall, enqueueAt);
        }
    }
}
