namespace ExamApp.Application.Common.Interfaces
{
    public interface IAutoSubmitExamJob
    {
        Task ExecuteAsync(string studentExamId, CancellationToken cancellationToken = default);
    }
}
