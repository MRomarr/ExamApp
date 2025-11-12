namespace ExamApp.Application.Features.Exam.Commands.AssignExamToUser
{
    public class AssignExamCommand : IRequest<Result>
    {
        public string ExamId { get; set; }
        public string UserId { get; set; }

    }

}
