namespace ExamApp.Application.Features.Exam.Commands.StartExam
{
    public class StartExamCommandHandler : IRequestHandler<StartExamCommand, Result<ExamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly ILogger<StartExamCommandHandler> _logger;

        public StartExamCommandHandler(
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            IExamRepository examRepository,
            IMapper mapper,
            IBackgroundJobService backgroundJobService,
            ILogger<StartExamCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _examRepository = examRepository;
            _mapper = mapper;
            _backgroundJobService = backgroundJobService;
            _logger = logger;
        }

        public async Task<Result<ExamDto>> Handle(StartExamCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<ExamDto>.Failure("User not authenticated.");

            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam == null)
                return Result<ExamDto>.Failure("Exam not found.");

            if (!exam.IsActive)
                return Result<ExamDto>.Failure("Exam is not active.");

            var studentExams = await _unitOfWork.StudentExams.GetAllAsync();
            var studentExam = studentExams.FirstOrDefault(se => se.ExamId == request.ExamId && se.StudentId == userId);

            if (studentExam is null)
                return Result<ExamDto>.Failure("Student exam not found.");

            if (studentExam.CompletedAt != null)
                return Result<ExamDto>.Failure("Exam already completed.");

            if (studentExam.StartedAt != null)
                return Result<ExamDto>.Failure("Exam already started.");

            studentExam.StartedAt = DateTime.UtcNow;
            _unitOfWork.StudentExams.Update(studentExam);

            var saved = await _unitOfWork.SaveAsync();
            if (!saved)
                return Result<ExamDto>.Failure("Failed to start exam.");

            // Schedule job through abstraction
            var autoSubmitTime = studentExam.StartedAt.Value.AddMinutes(exam.DurationInMinutes);
            var jobId = _backgroundJobService.Schedule<IAutoSubmitExamJob>(
                job => job.ExecuteAsync(studentExam.Id, CancellationToken.None),
                autoSubmitTime);

            _logger.LogInformation(
                "Exam {ExamId} started for {UserId}, auto-submit scheduled at {Time}, JobId {JobId}",
                request.ExamId, userId, autoSubmitTime, jobId);

            var examWithDetails = await _examRepository.GetExamWithDetailsAsync(request.ExamId);
            var examDto = _mapper.Map<ExamDto>(examWithDetails);

            return Result<ExamDto>.Success(examDto, "Exam started successfully.");
        }
    }
}
