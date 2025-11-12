namespace ExamApp.Application.DTOs
{
    public class ExamDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationInMinutes { get; set; }
        public double TotalMarks { get; set; }
        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();

    }
    public class QuestionDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public double Marks { get; set; }
        public ICollection<QuestionOptionDto> QuestionOptions { get; set; } = new List<QuestionOptionDto>();
    }
    public class QuestionOptionDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
