
namespace ExamApp.Application.Features.Question.Command.AddQuestion
{
    internal class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddQuestionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam == null)
            {
                return Result.Failure("Exam not found.");
            }
            var question = _mapper.Map<Domain.Entites.Question>(request);
            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveAsync();
            return Result.Success("Question added successfully.");
        }
    }
}
