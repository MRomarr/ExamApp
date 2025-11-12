namespace ExamApp.Application.Features.Exam.Queries.GetExamById
{
    public record GetExamByIdQuery(string Id) : IRequest<Result<ExamDto>>;

}
