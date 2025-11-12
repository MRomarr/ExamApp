namespace ExamApp.Application.Features.Question.Command.AddQuestion
{
    public class AddQuestionCommand : IRequest<Result>
    {
        public string ExamId { get; set; }
        public string Text { get; set; }
        public double Marks { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}
