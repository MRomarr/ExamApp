
namespace ExamApp.Application.Features.Question.Command.SubmitAnswer
{
    internal class SubmitAnswerCommandHandler : IRequestHandler<SubmitAnswerCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubmitAnswerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.Questions.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                return Result.Failure("Question not found.");
            }
            var studentAsnwers = await _unitOfWork.StudentAnswers.GetAllAsync();
            var existingAnswer = studentAsnwers
                .FirstOrDefault(sa => sa.QuestionId == request.QuestionId && sa.StudentExamId == request.ExamId);
            if (existingAnswer is not null)
            {
                _unitOfWork.StudentAnswers.Delete(existingAnswer);
            }
            var studentAnswer = new StudentAnswer
            {
                QuestionId = request.QuestionId,
                SelectedOptionIndex = request.SelectedOptionIndex,
                StudentExamId = request.ExamId
            };
            await _unitOfWork.StudentAnswers.AddAsync(studentAnswer);
            await _unitOfWork.SaveAsync();
            return Result.Success("Answer submitted successfully.");
        }
    }
}
