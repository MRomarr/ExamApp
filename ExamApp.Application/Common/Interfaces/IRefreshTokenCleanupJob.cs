
namespace ExamApp.Application.Common.Interfaces
{
    public interface IRefreshTokenCleanupJob
    {
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
