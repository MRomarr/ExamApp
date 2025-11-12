
namespace ExamApp.Application.Features.Exam.Commands.AssignExamToUser
{
    internal class AssignExamCommandHandler : IRequestHandler<AssignExamCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssignExamCommandHandler> _logger;
        public AssignExamCommandHandler(IUnitOfWork unitOfWork, ILogger<AssignExamCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result> Handle(AssignExamCommand request, CancellationToken cancellationToken)
        {
            var studentExams = await _unitOfWork.StudentExams.GetAllAsync();

            if (studentExams.Any(s => s.ExamId == request.ExamId && s.StudentId == request.UserId))
            {
                return Result.Failure("Exam already assigned to this user.");
            }

            var studentExam = new StudentExam
            {
                ExamId = request.ExamId,
                StudentId = request.UserId
            };

            await _unitOfWork.StudentExams.AddAsync(studentExam);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Exam {ExamId} assigned to user {UserId}", request.ExamId, request.UserId);
            return Result.Success("Exam assigned to user successfully.");
        }
    }
}
