
namespace ExamApp.Application.Common.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
