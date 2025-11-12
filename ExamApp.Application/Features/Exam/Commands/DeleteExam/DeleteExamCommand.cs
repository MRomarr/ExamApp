namespace ExamApp.Application.Features.Exam.Commands.DeleteExam
{
    public record DeleteExamCommand(string id) : IRequest<Result>;
}
