namespace ExamApp.Application.Features.Question.Command.SubmitAnswer
{
    public class SubmitAnswerCommand : IRequest<Result>
    {
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
        public int SelectedOptionIndex { get; set; }
    }
}
