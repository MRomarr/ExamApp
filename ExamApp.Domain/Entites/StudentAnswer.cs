namespace ExamApp.Domain.Entites
{
    public class StudentAnswer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentExamId { get; set; }
        public StudentExam StudentExam { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
        public int SelectedOptionIndex { get; set; }
    }
}