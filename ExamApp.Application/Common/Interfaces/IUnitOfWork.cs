namespace ExamApp.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Exam> Exams { get; }
        IRepository<StudentExam> StudentExams { get; }
        IRepository<Question> Questions { get; }
        IRepository<StudentAnswer> StudentAnswers { get; }
        Task<bool> SaveAsync();
    }
}
