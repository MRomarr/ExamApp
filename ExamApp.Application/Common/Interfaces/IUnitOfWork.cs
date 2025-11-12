namespace ExamApp.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Exam> Exams { get; }
        IRepository<StudentExam> StudentExams { get; }
        Task<bool> SaveAsync();
    }
}
