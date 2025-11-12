
namespace ExamApp.Application.Features.Question.Command.DeleteQuestion
{
    internal class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteQuestionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.Questions.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                return Result.Failure("Question not found.");
            }
            _unitOfWork.Questions.Delete(question);
            var result = await _unitOfWork.SaveAsync();
            if (!result)
            {
                return Result.Failure("Failed to delete question.");
            }
            return Result.Success("Question deleted successfully.");
        }
    }
}
