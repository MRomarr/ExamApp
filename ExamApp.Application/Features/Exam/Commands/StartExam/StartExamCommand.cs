namespace ExamApp.Application.Features.Exam.Commands.StartExam
{
    public class StartExamCommand : IRequest<Result<ExamDto>>
    {
        public string ExamId { get; set; }
    }
}
