namespace ExamApp.Domain.Entites
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Token { get; set; }

        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

        public bool IsUsed { get; set; } = false;
        public bool IsRevoked { get; set; } = false;

        public bool IsActive => !IsExpired && !IsRevoked && !IsUsed;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
