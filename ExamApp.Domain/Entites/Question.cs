namespace ExamApp.Domain.Entites
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public double Marks { get; set; }
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
    }
}
