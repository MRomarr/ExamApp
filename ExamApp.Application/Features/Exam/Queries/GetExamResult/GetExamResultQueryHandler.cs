

namespace ExamApp.Application.Features.Exam.Queries.GetExamResult
{
    internal class GetExamResultQueryHandler : IRequestHandler<GetExamResultQuery, Result<ExamResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        public GetExamResultQueryHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }
        public async Task<Result<ExamResultDto>> Handle(GetExamResultQuery request, CancellationToken cancellationToken)
        {
            var Exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (Exam == null)
                return Result<ExamResultDto>.Failure("Exam not found.");
            var studentId = _userContext.UserId;
            var studentExams = await _unitOfWork.StudentExams.GetAllAsync();
            var studentExam = studentExams.FirstOrDefault(se => se.ExamId == request.ExamId && se.StudentId == studentId);
            if (studentExam == null || studentExam.Score == null)
                return Result<ExamResultDto>.Failure("Exam result not found.");
            var examResultDto = new ExamResultDto { StudentMark = studentExam.Score.Value, TotalMark = Exam.TotalMarks };
            return Result<ExamResultDto>.Success(examResultDto);
        }
    }
}
