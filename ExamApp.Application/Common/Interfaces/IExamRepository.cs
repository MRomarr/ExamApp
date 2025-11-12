namespace ExamApp.Application.Common.Interfaces
{
    public interface IExamRepository
    {
        Task<Exam?> GetExamWithDetailsAsync(string id);
    }
}
