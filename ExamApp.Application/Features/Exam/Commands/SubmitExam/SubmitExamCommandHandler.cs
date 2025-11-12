namespace ExamApp.Application.Features.Exam.Commands.SubmitExam
{
    internal class SubmitExamCommandHandler : IRequestHandler<SubmitExamCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly ILogger<SubmitExamCommandHandler> _logger;

        public SubmitExamCommandHandler(
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            ILogger<SubmitExamCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task<Result> Handle(SubmitExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam == null)
                return Result.Failure("Exam not found.");

            if (!exam.IsActive)
                return Result.Failure("Cannot submit this exam.");

            var userId = _userContext.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result.Failure("User not authenticated.");

            var studentExams = await _unitOfWork.StudentExams.GetAllAsync();
            var studentExam = studentExams.FirstOrDefault(se => se.ExamId == request.ExamId && se.StudentId == userId);

            if (studentExam is null)
                return Result.Failure("Student exam not found.");

            if (studentExam.CompletedAt != null)
                return Result.Failure("Exam has already been submitted.");

            if (studentExam.StartedAt == null)
                return Result.Failure("Exam has not been started yet.");

            var elapsedMinutes = (DateTime.UtcNow - studentExam.StartedAt.Value).TotalMinutes;
            if (elapsedMinutes > exam.DurationInMinutes)
                return Result.Failure("Exam time has expired. The exam has been automatically submitted.");

            studentExam.CompletedAt = DateTime.UtcNow;
            studentExam.Score = 0;

            _unitOfWork.StudentExams.Update(studentExam);
            var result = await _unitOfWork.SaveAsync();

            if (!result)
                return Result.Failure("Failed to submit the exam.");

            _logger.LogInformation(
                "Student {UserId} submitted exam {ExamId} with score {Score}",
                userId, request.ExamId, studentExam.Score);

            return Result.Success("Exam submitted succes");
        }
    }
}