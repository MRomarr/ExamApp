namespace ExamApp.Application.Features.Question.Command.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest<Result>
    {
        public string QuestionId { get; set; }
    }
}