namespace ExamApp.Application.Features.Exam.Commands.CreateExam
{
    internal class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, Result<ExamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateExamCommandHandler> _logger;

        public CreateExamCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateExamCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ExamDto>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            if (request.StartDate < DateTime.UtcNow)
                return Result<ExamDto>.Failure("Exam cannot start in the past.");

            var exam = _mapper.Map<Domain.Entites.Exam>(request);

            await _unitOfWork.Exams.AddAsync(exam);

            var saveResult = await _unitOfWork.SaveAsync();

            if (!saveResult)
            {
                _logger.LogError("Failed to create exam {@Exam}", exam);
                return Result<ExamDto>.Failure("Failed to create exam");
            }

            var examDto = _mapper.Map<ExamDto>(exam);

            _logger.LogInformation("Exam created successfully with Id {ExamId}", exam.Id);

            return Result<ExamDto>.Success(examDto, "Exam created successfully");
        }
    }
}
