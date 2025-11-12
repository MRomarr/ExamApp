namespace ExamApp.Application.Features.Exam.Queries.GetExamByIdWithDetails
{
    public record GetExamByIdWithDetailsQuery(string Id) : IRequest<Result<ExamDto>>;
}
