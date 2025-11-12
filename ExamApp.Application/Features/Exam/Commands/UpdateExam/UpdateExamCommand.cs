namespace ExamApp.Application.Features.Exam.Commands.UpdateExam
{
    public class UpdateExamCommand : IRequest<Result<ExamDto>>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationInMinutes { get; set; }
        public double TotalMarks { get; set; }
        //public ICollection<UpdateQuestionCommand>? Questions { get; set; } = new List<UpdateQuestionCommand>();
    }

    //public class UpdateQuestionCommand
    //{
    //    public string? Id { get; set; }
    //    public string Text { get; set; }
    //    public double Marks { get; set; }
    //    public ICollection<UpdateQuestionOptionCommand> QuestionOptions { get; set; } = new List<UpdateQuestionOptionCommand>();
    //}

    //public class UpdateQuestionOptionCommand
    //{
    //    public string? Id { get; set; }
    //    public string Text { get; set; }
    //    public bool IsCorrect { get; set; }
    //}
}
