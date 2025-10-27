
namespace ExamApp.Application.Common.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task SaveChangesAsync();
    }
}
