using System.Linq.Expressions;

namespace ExamApp.Application.Common.Interfaces
{
    public interface IBackgroundJobService
    {
        string Schedule<TJob>(Expression<Func<TJob, Task>> methodCall, DateTimeOffset enqueueAt)
            where TJob : class;
    }
}
