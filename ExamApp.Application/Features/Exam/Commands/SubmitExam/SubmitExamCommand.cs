namespace ExamApp.Application.Features.Exam.Commands.SubmitExam
{
    public class SubmitExamCommand : IRequest<Result>
    {
        public string ExamId { get; set; }
    }
}
