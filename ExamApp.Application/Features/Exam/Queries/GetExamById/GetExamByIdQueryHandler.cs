

namespace ExamApp.Application.Features.Exam.Queries.GetExamById
{
    internal class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, Result<ExamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetExamByIdQueryHandler> _logger;
        public GetExamByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetExamByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<ExamDto>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(request.Id);
            if (exam is null)
            {
                _logger.LogWarning("Exam with id: {ExamId} not found.", request.Id);
                return Result<ExamDto>.Failure($"Exam not found.");
            }

            _logger.LogInformation("Exam with id: {ExamId} retrieved successfully.", request.Id);
            var examDto = _mapper.Map<ExamDto>(exam);
            return Result<ExamDto>.Success(examDto);
        }
    }
}
