namespace ExamApp.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user);
        Task<string> GenerateAndStoreRefreshTokenAsync(ApplicationUser user);
    }
}
