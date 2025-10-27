using Microsoft.AspNetCore.Identity;

namespace ExamApp.Domain.Entites
{
    public class ApplicationUser : IdentityUser
    {
        ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();

    }
}
