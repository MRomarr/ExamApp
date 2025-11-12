

namespace ExamApp.Application.Features.Exam.Queries.GetExamByIdWithDetails
{
    internal class GetExamByIdWithDetailsQueryHandler : IRequestHandler<GetExamByIdWithDetailsQuery, Result<ExamDto>>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetExamByIdWithDetailsQueryHandler> _logger;

        public GetExamByIdWithDetailsQueryHandler(IExamRepository examRepository, IMapper mapper, ILogger<GetExamByIdWithDetailsQueryHandler> logger)
        {
            _examRepository = examRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ExamDto>> Handle(GetExamByIdWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.GetExamWithDetailsAsync(request.Id);
            if (exam == null)
            {
                _logger.LogWarning("Exam with ID {ExamId} not found.", request.Id);
                return Result<ExamDto>.Failure($"Exam with ID {request.Id} not found.");
            }
            var examDto = _mapper.Map<ExamDto>(exam);
            return Result<ExamDto>.Success(examDto);
        }
    }
}

