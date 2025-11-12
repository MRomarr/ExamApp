using ExamApp.Application.Features.Exam.Commands.UpdateExam;

namespace ExamApp.Application.Features.Exam.Commands
{
    internal class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, Result<ExamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateExamCommandHandler> _logger;

        public UpdateExamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateExamCommandHandler> logger, IExamRepository examRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _examRepository = examRepository;
        }

        public async Task<Result<ExamDto>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.GetExamWithDetailsAsync(request.Id);
            if (exam == null)
                return Result<ExamDto>.Failure("Exam not found.");

            if (exam.StartDate <= DateTime.UtcNow)
                return Result<ExamDto>.Failure("Exam has already started and cannot be edited.");

            _mapper.Map(request, exam);


            _unitOfWork.Exams.Update(exam);
            var success = await _unitOfWork.SaveAsync();

            if (!success)
            {
                _logger.LogError("Failed to update exam {ExamId}", exam.Id);
                return Result<ExamDto>.Failure("Failed to update exam.");
            }

            var examDto = _mapper.Map<ExamDto>(exam);
            return Result<ExamDto>.Success(examDto, "Exam updated successfully.");
        }
    }
}
