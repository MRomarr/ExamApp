namespace ExamApp.Application.Features.Exam.Queries.GetExamResult
{
    public class GetExamResultQuery : IRequest<Result<ExamResultDto>>
    {
        public string ExamId { get; set; }
    }
}
