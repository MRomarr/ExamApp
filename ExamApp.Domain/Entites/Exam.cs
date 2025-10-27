namespace ExamApp.Domain.Entites
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        public double TotalMarks { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }
}
