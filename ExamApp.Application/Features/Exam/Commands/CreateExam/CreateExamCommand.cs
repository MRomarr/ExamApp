namespace ExamApp.Application.Features.Exam.Commands.CreateExam
{
    public class CreateExamCommand : IRequest<Result<ExamDto>>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationInMinutes { get; set; }
        public double TotalMarks { get; set; }

        public ICollection<CreateQuestionCommand> Questions { get; set; } = new List<CreateQuestionCommand>();
    }

    public class CreateQuestionCommand
    {
        public string Text { get; set; } = string.Empty;
        public double Marks { get; set; }
        public ICollection<CreateQuestionOptionCommand> QuestionOptions { get; set; } = new List<CreateQuestionOptionCommand>();
    }

    public class CreateQuestionOptionCommand
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
