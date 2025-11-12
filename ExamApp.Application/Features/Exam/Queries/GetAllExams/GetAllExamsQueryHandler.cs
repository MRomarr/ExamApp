namespace ExamApp.Application.Features.Exam.Queries.GetAllExams
{
    internal class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsQuery, List<ExamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllExamsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ExamDto>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
            var exams = await _unitOfWork.Exams.GetAllAsync();
            return _mapper.Map<List<ExamDto>>(exams);
        }
    }
}
