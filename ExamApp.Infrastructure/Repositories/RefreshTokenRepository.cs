namespace ExamApp.Infrastructure.Repositories
{
    internal class RefreshTokenRepository(ApplicationDbContext dbContext) : IRefreshTokenRepository
    {
        public async Task AddAsync(RefreshToken token)
        {
            await dbContext.RefreshTokens.AddAsync(token);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

    }
}
