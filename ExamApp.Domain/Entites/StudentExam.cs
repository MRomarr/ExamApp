namespace ExamApp.Domain.Entites
{
    public class StudentExam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public double? Score { get; set; }

        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }
}
