namespace ExamApp.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {

        Task<bool> SaveAsync();
    }
}
